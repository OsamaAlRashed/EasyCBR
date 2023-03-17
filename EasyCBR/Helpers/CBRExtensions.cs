using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Linq.Expressions;

namespace EasyCBR.Helpers;

public static class CBRExtensions
{
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
