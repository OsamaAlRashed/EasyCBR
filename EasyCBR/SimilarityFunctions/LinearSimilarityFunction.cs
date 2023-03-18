using EasyCBR.SimilarityFunctions.Base;
using System.Collections.Generic;
using System.Numerics;

namespace EasyCBR.SimilarityFunctions;

/// <summary>
/// Represents linear similarity function.
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public sealed class LinearSimilarityFunction<TProperty> : SimilarityFunction
     where TProperty : INumber<TProperty>
{
    internal override int Weight { get; set; }
    internal override List<double> Scores { get; set; }

    public LinearSimilarityFunction(int weight = 1) : base(weight) { }

    internal override double Invoke<TCase>(object value, object newValue)
            where TCase : class 
        => (double)value - (double)newValue;
}
