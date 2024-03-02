using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCBR.Helpers;

internal static class HelperMethods
{
    internal static Dictionary<string, Type> GetNameAndTypeProperties<TEntity>()
    {
        var values = new Dictionary<string, Type>();
        var props = typeof(TEntity).GetProperties();

        foreach (PropertyInfo prop in props) 
        {
            values.Add(prop.Name, prop.PropertyType);
        }

        return values;
    }

    internal static object GetPropertyValue(object obj, string propName) 
        => obj.GetType().GetProperty(propName).GetValue(obj, null);

    internal static Type GetTypeFromMemberInfo(this MemberInfo member)
        => (member as PropertyInfo).PropertyType;

    internal static Dictionary<string, TProperty> EnumToDictionary<TProperty>()
    {
        var names = Enum.GetNames(typeof(TProperty)).Cast<string>();
        var values = Enum.GetValues(typeof(TProperty)).Cast<TProperty>();

        return names.Zip(values).ToDictionary(k => k.First, v => v.Second);
    }

}
