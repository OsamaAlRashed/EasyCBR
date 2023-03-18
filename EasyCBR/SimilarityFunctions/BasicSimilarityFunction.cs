using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;

namespace EasyCBR.SimilarityFunctions;

/// <summary>
/// Represents basic similarity function.
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public sealed class BasicSimilarityFunction<TProperty> : SimilarityFunction
    where TProperty : IEquatable<TProperty>, IComparable<TProperty>
{
    public BasicSimilarityFunction(int weight = 1) : base(weight) { }

    internal override int Weight { get; set; }
    internal override List<double> Scores { get; set; }

    internal override double Invoke<TCase>(object value, object newValue)
        where TCase : class 
        => value.Equals(newValue) ? 1 : 0;
}
