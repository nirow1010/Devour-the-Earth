using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(MonoBehaviour), true)]
public class SyncedArrayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MonoBehaviour targetObj = (MonoBehaviour) target;
        Type type = targetObj.GetType();

        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            SerializedProperty prop = serializedObject.FindProperty(field.Name);
            SyncedArrayAttribute syncedAttr = field.GetCustomAttribute<SyncedArrayAttribute>();

            if (prop != null)
            {
                if (syncedAttr != null && prop.isArray)
                {
                    SerializedProperty sizeProp = serializedObject.FindProperty(syncedAttr.sizeFieldName);

                    if (sizeProp != null && sizeProp.propertyType == SerializedPropertyType.Integer)
                    {
                        int newSize = Mathf.Max(0, sizeProp.intValue);
                        if (prop.arraySize != newSize)
                            prop.arraySize = newSize;
                    }
                    else
                    {
                        EditorGUILayout.HelpBox($"Could not find valid size field '{syncedAttr.sizeFieldName}' for array '{field.Name}'.", MessageType.Warning);
                    }
                }

                EditorGUILayout.PropertyField(prop, true);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
