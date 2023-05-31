using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Linq.Expressions;
using System.Numerics;

namespace EasyCBR.Helpers;

/// <summary>
/// Represents extension methods to CBR.
/// </summary>
public static class CBRExtensions
{
    /// <summary>
    /// Determines the output property.
    /// </summary>
    /// <typeparam name="TCase">Case</typeparam>
    /// <typeparam name="TProperty">Property</typeparam>
    /// <param name="entity">Instance of CBR</param>
    /// <param name="propertyExpression">Select the property</param>
    /// <returns>Returns CBR instance eith a selected case.</returns>
    public static ModelWithOutput<TCase, TOutput> Output<TCase, TOutput, TProperty>(this CBR<TCase, TOutput> entity, Expression<Func<TCase, TProperty>> propertyExpression)
        where TCase : class
        where TProperty : INumber<TProperty>
    {
        if(typeof(TProperty) != typeof(TOutput))
        {
            throw new ArgumentException("The type of the property must match the type of the output");
        }

        var memberExpression = (MemberExpression)propertyExpression.Body;
        return new ModelWithOutput<TCase, TOutput>()
        {
            Case = entity,
            Output = memberExpression.Member
        };
    }

    /// <summary>
    /// Set Similarity functions to properties.
    /// </summary>
    /// <typeparam name="TCase">Case</typeparam>
    /// <param name="entityWithOutput">The return type of output.</param>
    /// <param name="pairs">Each propetry with its similarity function.</param>
    /// <returns></returns>
    public static CBR<TCase, TOutput> SetSimilarityFunctions<TCase, TOutput>(this ModelWithOutput<TCase, TOutput> entityWithOutput, params (string property, SimilarityFunction similarityFunction)[] pairs)
        where TCase : class
    {
        if (pairs == null)
            throw new ArgumentNullException(nameof(pairs));

        if(pairs.Length == 0)
            throw new ArgumentException(nameof(pairs));

        entityWithOutput.Case.TargetProperty = new TargetProperty()
        {
            Name = entityWithOutput.Output.Name,
            Type = entityWithOutput.Output.GetTypeFromMemberInfo(),
        };

        foreach (var (property, similarityFunction) in pairs)
        {
            if(!entityWithOutput.Case.Properties.ContainsKey(property))
                throw new ArgumentException(nameof(property));

            if (entityWithOutput.Case.Properties[property] != similarityFunction.GetType().GetGenericArguments()[0])
                throw new ArgumentException(nameof(property));

            entityWithOutput.Case.SimilarityFunctionsPerProperties.TryAdd(property, similarityFunction);
        }

        return entityWithOutput.Case;
    }
}
