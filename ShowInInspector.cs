using System;
using System.Reflection;

namespace UnityEngine
{
    /// <summary>
    /// Makes a variable show up in the inspector.
    /// </summary>
    public sealed class ShowInInspector : Attribute
    {
    }

    /// <summary>
    /// Helps to access the ShowInInspector and HideInInspector attributes of members.
    /// </summary>
    public static class ShowInInspectorExtension
    {
        /// <summary>
        /// Returns if the field has a ShowInInspector attribute.
        /// </summary>
        public static bool GetShowInInspector(this FieldInfo value)
        {
            ShowInInspector[] attributes = value.GetCustomAttributes(typeof(ShowInInspector), true) as ShowInInspector[];
            return (attributes.Length > 0);
        }

        /// <summary>
        /// Returns if the property has a ShowInInspector attribute.
        /// </summary>
        public static bool GetShowInInspector(this PropertyInfo value)
        {
            ShowInInspector[] attributes = value.GetCustomAttributes(typeof(ShowInInspector), true) as ShowInInspector[];
            return (attributes.Length > 0);
        }

        /// <summary>
        /// Returns if the field has a HideInInspector attribute.
        /// </summary>
        public static bool GetHideInInspector(this FieldInfo value)
        {
            HideInInspector[] attributes = value.GetCustomAttributes(typeof(HideInInspector), true) as HideInInspector[];
            return (attributes.Length > 0);
        }

        /// <summary>
        /// Returns if the property has a HideInInspector attribute.
        /// </summary>
        public static bool GetHideInInspector(this PropertyInfo value)
        {
            HideInInspector[] attributes = value.GetCustomAttributes(typeof(HideInInspector), true) as HideInInspector[];
            return (attributes.Length > 0);
        }
    }
}