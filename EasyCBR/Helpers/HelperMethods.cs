using System;
using System.Collections.Generic;
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

    internal static Type GetTypeFromMemberInfo(this MemberInfo member)
        => (member as PropertyInfo).PropertyType;

}
