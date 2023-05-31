using EasyCBR.Helpers;
using System;
using System.Collections.Generic;

namespace EasyCBR.SimilarityFunctions.Base;

/// <summary>
/// Represents the abstract similarity function.
/// </summary>
public abstract class SimilarityFunction
{
    public SimilarityFunction(int weight = 1)
    {
        Weight = weight;
        Scores = new List<double>();
    }

    internal abstract int Weight { get; set; }
    internal abstract List<double> Scores { get; set; }

    internal void InvokeCore<TCase, TOutput>(CBR<TCase, TOutput> cbr, string propertyName) where TCase : class
    {
        if (cbr == null)
            throw new ArgumentNullException(nameof(cbr));

        if (propertyName == null)
            throw new ArgumentNullException(propertyName);

        if (!cbr.Properties.ContainsKey(propertyName))
            throw new ArgumentException(propertyName);


        var newCasePropertyValue = HelperMethods.GetPropertyValue(cbr.Case, propertyName);

        for (int i = 0; i < cbr.Cases.Count; i++)
        {
            var value = HelperMethods.GetPropertyValue(cbr.Cases[i], propertyName);

            Scores.Add(Invoke<TCase>(value, newCasePropertyValue));
        }
    }

    internal abstract double Invoke<TCase>(object value, object newValue) where TCase : class;
}
