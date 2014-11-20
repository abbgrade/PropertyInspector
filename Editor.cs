using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PropertyInspector
{
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true)]
    public class Editor : UnityEditor.Editor
    {
        #region Foldout States

        private bool foldoutSerializedState = true;

        private static Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();

        #endregion Foldout States

        #region Utils

        /// <summary>
        ///     Creates recursively EditorGUI elements for an abitrary object.
        /// </summary>
        private void DrawInspectedMembers(string label, object target, List<string> path, bool root = false)
        {
            // The unique path of member labels is used as key to save the foldout state.
            path = new List<string>(path);
            path.Add(label);
            var key = string.Join("\\", path.ToArray());

            foldoutStates[key] = EditorGUILayout.Foldout(foldoutStates.ContainsKey(key) ? foldoutStates[key] : root, root ? label : string.Format("{0} ({1})", label, target.GetType().Name));
            if (foldoutStates[key])
            {
                EditorGUI.indentLevel++;

                var targetType = target.GetType();
                // Collect all members that may be displayed. Private or hidden members are left out in a later step.
                var members = new List<System.Reflection.MemberInfo>();
                if (!root) members.AddRange(targetType.GetFields()); // The fields of the root element is already displayed by the default inspector.
                members.AddRange(targetType.GetProperties());
                int visibleMemberCount = 0;

                foreach (var member in members)
                {
                    string name = member.Name;

                    // Get the type, value and skip the member if it should not be displayed.
                    object value;
                    System.Type type_;
                    if (member is System.Reflection.FieldInfo)
                    {
                        var field = member as System.Reflection.FieldInfo;

                        // A field will be shown if it is public or has a ShowInInspector attribute and not has an HideInInspector attribute.
                        if (!(field.IsPublic || field.GetShowInInspector()) || field.GetHideInInspector()) continue;

                        value = field.GetValue(target);
                        type_ = field.FieldType;
                    }
                    else if (member is System.Reflection.PropertyInfo)
                    {
                        var property = member as System.Reflection.PropertyInfo;

                        // A property will be shown if it has an getter method and if it has an ShowInInspector attribute. For technical reasons is must be public.
                        if (property.GetGetMethod(nonPublic: true) == null && property.GetGetMethod(nonPublic: false) == null) continue;
                        if (!property.GetShowInInspector()) continue;

                        value = property.GetValue(target, null);
                        type_ = property.PropertyType;
                    }
                    else
                    {
                        // This case will not appear.
                        throw new NotImplementedException();
                    }

                    visibleMemberCount++;

                    // Show EditorGUI element.
                    if (type_.IsSubclassOf(typeof(MonoBehaviour)))
                    {
                        // Elements with an own inspector will be shown as reference.
                        EditorGUILayout.ObjectField(name, (MonoBehaviour)value, type_, allowSceneObjects: true);
                    }
                    else if (value == null)
                    {
                        // Null elements are listed with their type.
                        EditorGUILayout.LabelField(label: name, label2: "None (" + type_ + ")");
                    }
                    else if (type_.IsClass && !(value is string))
                    {
                        // Non primitive types are recursively displayed with a new foldout and indent level in the inspector.
                        DrawInspectedMembers(name, value, path);
                    }
                    else
                    {
                        // Members with primitive types are shown as labels.
                        EditorGUILayout.LabelField(label: name, label2: value.ToString());
                    }
                }

                if (visibleMemberCount == 0)
                    EditorGUILayout.LabelField("No visible member.");

                EditorGUI.indentLevel--;
            }
        }

        #endregion Utils

        public override void OnInspectorGUI()
        {
            // Draw the default inspector with the serializable members.
            DrawDefaultInspector();

            // Draw the non serializable members recursively.
            DrawInspectedMembers("Volatile Members", target, path: new List<string>(), root: true);

            Repaint();
        }
    }
}