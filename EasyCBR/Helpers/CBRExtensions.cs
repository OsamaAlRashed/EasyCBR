using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Linq.Expressions;

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
    public static ModelWithOutput<TCase> Output<TCase, TProperty>(this CBR<TCase> entity, Expression<Func<TCase, TProperty>> propertyExpression)
        where TCase : class
    {
        var memberExpression = (MemberExpression)propertyExpression.Body;
        return new ModelWithOutput<TCase>() 
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
    public static CBR<TCase> SetSimilarityFunctions<TCase>(this ModelWithOutput<TCase> entityWithOutput, params (string property, SimilarityFunction similarityFunction)[] pairs)
        where TCase : class
    {
        entityWithOutput.Case.TargetProperty = new TargetProperty()
        {
            Name = entityWithOutput.Output.Name,
            Type = entityWithOutput.Output.GetTypeFromMemberInfo(),
        };

        foreach (var (property, similarityFunction) in pairs)
        {
            entityWithOutput.Case.SimilarityFunctionsPerProperties.TryAdd(property, similarityFunction);
        }

        return entityWithOutput.Case;
    }
}
