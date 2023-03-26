using EasyCBR.SimilarityFunctions.Base;
using System;
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

    private double maxDiffrence = 1;

    public LinearSimilarityFunction(int weight = 1) : base(weight) { }

    internal override double Invoke<TCase>(object value, object newValue)
            where TCase : class
    {
        var diffrence = Math.Abs(double.Parse(value.ToString()) - double.Parse(newValue.ToString()));

        // ToDo
        if (diffrence > maxDiffrence)
        {
            maxDiffrence = diffrence;
        }

        return diffrence;
    }
}
