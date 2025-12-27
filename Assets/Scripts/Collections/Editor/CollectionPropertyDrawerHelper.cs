using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor.Rendering;
using System;

namespace MChojniak.Collections.Editor
{   
    public static class CollectionPropertyDrawerHelper
    {   
        public static void ClearSerializedProperty(this SerializedProperty prop)
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
