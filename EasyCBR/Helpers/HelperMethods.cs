using EasyCBR.SimilarityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EasyCBR.Helpers
{
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
        { 
            return obj.GetType().GetProperty(propName).GetValue(obj, null); 
        }

        internal static (string, Type) GetPropertyHasCustomAttribute<TCase, TAttribute>()
            where TCase : class
            where TAttribute : Attribute
        {
            Type caseType = typeof(TCase);

            var properties = caseType.GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(TAttribute), true).Length > 0)
                .ToList();

            if (properties.Count == 0)
                throw new Exception();

            if (properties.Count > 1)
                throw new Exception();

            return (properties[0].Name, properties[0].PropertyType);
        }

    }

    public static class CBRExtensions
    {
        public static (CBR<T>, MemberInfo) Output<T, U>(this CBR<T> entity, Expression<Func<T, U>> propertyExpression)
            where T : class
        {
            var memberExpression = (MemberExpression)propertyExpression.Body;
            return (entity, memberExpression.Member);
        }

        public static CBR<T> SetSimilarityFunctions<T>(this (CBR<T>, MemberInfo) entityWithOutput, params (string property, SimilarityFunction similarityFunction)[] pairs)
            where T : class
        {
            //entityWithOutput.Item1.TargetProperty = entityWithOutput.Item2;
            foreach (var (property, similarityFunction) in pairs)
            {
                entityWithOutput.Item1.SimilarityFunctionsPerProperties.TryAdd(property, similarityFunction);
            }

            return entityWithOutput.Item1;
        }
    }
}
