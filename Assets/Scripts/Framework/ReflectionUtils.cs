using System;
using System.Reflection;

public static class ReflectionUtils
{
    public static T GetPrivateField<T>(object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        return (T)field?.GetValue(obj);
    }

    public static void SetPrivateField(object obj, string fieldName, object value)
    {
        var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        field?.SetValue(obj, value);
    }

    public static void SetPrivateBackingField(object obj, string propertyName, object value)
    {
        var type = obj.GetType();
        FieldInfo field = null;

        while (type != null)
        {
            field = type.GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null) break;
            type = type.BaseType;
        }

        if (field == null)
            throw new Exception($"Backing field for property '{propertyName}' not found in type hierarchy.");

        field.SetValue(obj, value);
    }

}
