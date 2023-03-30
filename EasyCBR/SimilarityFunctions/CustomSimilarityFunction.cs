﻿using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;

namespace EasyCBR.SimilarityFunctions;

public sealed class CustomSimilarityFunction<TProperty> : SimilarityFunction
where TProperty : IEquatable<TProperty>
{
    public CustomSimilarityFunction(Func<TProperty, TProperty, double> function, int weight = 1) : base(weight)
        => Function = function;

    internal Func<TProperty, TProperty, double> Function { get; set; }
    internal override int Weight { get; set; }
    internal override List<double> Scores { get; set; }

    internal override double Invoke<TCase>(object value, object newValue)
        where TCase : class
        => Function((TProperty)value, (TProperty)newValue);
}
