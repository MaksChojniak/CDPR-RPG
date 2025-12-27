using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor.Rendering;
using System;

namespace MChojniak.Collections.Editor
{   
    [CustomPropertyDrawer(typeof(ISerializableStack), true)]
    public class UnityStackPropertyDrawer : PropertyDrawer 
    {
        SerializedProperty valuesProperty;

        float lineHeight;
        float verticalSpacing;

        ReorderableList list;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            valuesProperty = property.FindPropertyRelative("_serializedValues");

            lineHeight = EditorGUIUtility.singleLineHeight;
            verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.BeginProperty(position, label, property);

            list = DrawStack(position, property, label, valuesProperty);
            if(list is null)
            {
                property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, lineHeight), property.isExpanded, label, true);
                if(property.isExpanded)
                    EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight + verticalSpacing, position.width, lineHeight), "Stack is Empty");
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

        static ReorderableList DrawStack(Rect position, SerializedProperty property, GUIContent label, SerializedProperty valuesProperty)
        {
            if (property == null) 
                return null;

            if (valuesProperty == null)
                return null;

            var list = new ReorderableList(property.serializedObject, valuesProperty, false, true, true, true)
            {
                elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
            };
            list.drawNoneElementCallback += rect =>  EditorGUI.LabelField(rect, "Stack is Empty");

            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, label.text);
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var value = valuesProperty.GetArrayElementAtIndex(index);

                float lineHeight = EditorGUIUtility.singleLineHeight;
                float spacing = EditorGUIUtility.standardVerticalSpacing;

                Rect r = new Rect(rect.x, rect.y + 2, rect.width, lineHeight);
                EditorGUI.PropertyField(r, value, GUIContent.none);
            };

            list.onAddCallback = (ReorderableList l) =>
            {
                var so = property.serializedObject;
                so.Update();

                int index = valuesProperty.arraySize;
                valuesProperty.arraySize++;

                var newValue = valuesProperty.GetArrayElementAtIndex(index);
                newValue.ClearSerializedProperty();

                so.ApplyModifiedProperties();
            };

            list.onRemoveCallback = (ReorderableList l) =>
            {
                if (valuesProperty.arraySize <= 0) 
                    return;
                
                valuesProperty.DeleteArrayElementAtIndex(valuesProperty.arraySize - 1);

                l.index = Mathf.Max(0, valuesProperty.arraySize - 1);

                property.serializedObject.ApplyModifiedProperties();
            };
            
            return list;
        }

    }
}
