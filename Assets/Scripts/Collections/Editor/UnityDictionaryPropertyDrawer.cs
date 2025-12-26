using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor.Rendering;
using System;

namespace MChojniak.Collections.Editor
{   
    [CustomPropertyDrawer(typeof(ISerializableCollections), true)]
    public class UnityDictionaryPropertyDrawer : PropertyDrawer 
    {
        SerializedProperty keysProperty;
        SerializedProperty valuesProperty;

        float lineHeight;
        float verticalSpacing;

        ReorderableList list;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            keysProperty = property.FindPropertyRelative("_serializedKeys");
            valuesProperty = property.FindPropertyRelative("_serializedValues");

            lineHeight = EditorGUIUtility.singleLineHeight;
            verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.BeginProperty(position, label, property);

            list = DrawDictionary(position, property, label, keysProperty, valuesProperty);
            if(list is null)
            {
                property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, lineHeight), property.isExpanded, label, true);
                if(property.isExpanded)
                    EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight + verticalSpacing, position.width, lineHeight), "Dictionary is Empty");
            }
            else
            {
                list.DoList(position);
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (list is null)
                return lineHeight;
            return list.GetHeight();
        }

        static ReorderableList DrawDictionary(Rect position, SerializedProperty property, GUIContent label, SerializedProperty keysProperty, SerializedProperty valuesProperty)
        {
            if (property == null) 
                return null;

            if (keysProperty == null || valuesProperty == null)
                return null;

            var list = new ReorderableList(property.serializedObject, keysProperty, false, true, true, true)
            {
                elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
            };
            list.drawNoneElementCallback += rect =>  EditorGUI.LabelField(rect, "Dictionary is Empty");

            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, label.text);
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var key = keysProperty.GetArrayElementAtIndex(index);
                var value = valuesProperty.GetArrayElementAtIndex(index);

                float lineHeight = EditorGUIUtility.singleLineHeight;
                float spacing = EditorGUIUtility.standardVerticalSpacing;

                Rect r = new Rect(rect.x, rect.y + 2, rect.width, lineHeight);
                float half = (rect.width - 8) / 2f;
                Rect keyRect = new Rect(r.x, r.y, half, lineHeight);
                Rect valRect = new Rect(r.x + half + 8, r.y, half, lineHeight);

                EditorGUI.PropertyField(keyRect, key, GUIContent.none);
                EditorGUI.PropertyField(valRect, value, GUIContent.none);
            };

            list.onAddCallback = (ReorderableList l) =>
            {
                var so = property.serializedObject;
                so.Update();

                int index = keysProperty.arraySize;
                keysProperty.arraySize++;
                valuesProperty.arraySize++;

                var newKey = keysProperty.GetArrayElementAtIndex(index);
                var newValue = valuesProperty.GetArrayElementAtIndex(index);

                ClearSerializedProperty(newKey);
                ClearSerializedProperty(newValue);

                so.ApplyModifiedProperties();
            };

            list.onRemoveCallback = (ReorderableList l) =>
            {
                int index = l.index;
                if (index < 0) 
                    return;
                
                keysProperty.DeleteArrayElementAtIndex(index);
                valuesProperty.DeleteArrayElementAtIndex(index);

                l.index = Mathf.Max(0, keysProperty.arraySize - 1);

                property.serializedObject.ApplyModifiedProperties();
            };
            
            return list;
        }

        static void ClearSerializedProperty(SerializedProperty prop)
        {
            switch (prop.propertyType)
            {
                case SerializedPropertyType.String:
                    prop.stringValue = string.Empty;
                    break;
                case SerializedPropertyType.Integer:
                    prop.intValue = 0;
                    break;
                case SerializedPropertyType.Float:
                    prop.floatValue = 0f;
                    break;
                case SerializedPropertyType.Boolean:
                    prop.boolValue = false;
                    break;
                case SerializedPropertyType.Enum:
                    prop.enumValueIndex = 0;
                    break;
                case SerializedPropertyType.ObjectReference:
                    prop.objectReferenceValue = null;
                    break;
                default:
                    try { prop.managedReferenceValue = null; } catch { }
                    break;
            }
        }

    }
}
