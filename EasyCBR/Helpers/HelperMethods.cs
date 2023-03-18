using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCBR.Helpers;

internal static class HelperMethods
{
    internal static Dictionary<string, Type> GetNameAndTypeProperties<TEntiy>()
    {
        var values = new Dictionary<string, Type>();
        var props = typeof(TEntiy).GetProperties();

        foreach (PropertyInfo prop in props) 
        {
            values.Add(prop.Name, prop.PropertyType);
        }

        return values;
    }

    internal static object GetPropertyValue(object obj, string propName) 
        => obj.GetType().GetProperty(propName).GetValue(obj, null);

    internal static (string, Type) GetPropertyHasCustomAttribute<TCase, TAttribute>()
        where TCase : class
        where TAttribute : Attribute
    {
        Type caseType = typeof(TCase);

        var properties = caseType.GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(TAttribute), true).Length > 0)
            .ToList();

        if (properties.Count == 0)
            throw new Exception("Must put Output attribute on one of properties.");

        if (properties.Count > 1)
            throw new Exception("Must put Output attribute on one of properties.");

        //ToDO
        // Check if INumber type.

        return (properties[0].Name, properties[0].PropertyType);
    }

    internal static Type GetTypeFromMemberInfo(this MemberInfo member)
        => (member as PropertyInfo).PropertyType;

}
