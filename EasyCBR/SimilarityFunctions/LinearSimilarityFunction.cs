using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace EasyCBR.SimilarityFunctions;

public sealed class LinearSimilarityFunction<TProperty> : SimilarityFunction
     where TProperty : INumber<TProperty>
{
    private readonly double _min;
    private readonly double _max;
    private readonly double _rangeLength;

    internal override int Weight { get; set; }
    internal override List<double> Scores { get; set; }

    public LinearSimilarityFunction(double min, double max, int weight = 1) : base(weight)
    {
        if(min > max)
        {
            throw new ArgumentException("max should be greater than min");
        }

        _min = min;
        _max = max;
        _rangeLength = _max - _min + 1;
    }

    internal override double Invoke<TCase>(object value, object newValue)
    {
        var diffrence = Math.Abs(double.Parse(value.ToString()) - double.Parse(newValue.ToString()));

        double result = 1 - (diffrence / _rangeLength);
        return Math.Round(result, 2);
    }
}
