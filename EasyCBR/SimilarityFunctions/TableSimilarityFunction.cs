using EasyCBR.Enums;
using EasyCBR.Helpers;
using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;

namespace EasyCBR.SimilarityFunctions;

/// <summary>
/// Represents Table similarity function.
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public sealed class TableSimilarityFunction<TProperty> : SimilarityFunction
    where TProperty : Enum
{
    private Table<TProperty> _similarityTable;
    private Dictionary<string, TProperty> _enumAsDictionary = HelperMethods.EnumToDictionary<TProperty>();

    internal override int Weight { get; set; }
    internal override List<double> Scores { get; set; }

    public TableSimilarityFunction(int weight = 1) : base(weight) 
    {
        _similarityTable = new Table<TProperty>();
    }

    public TableSimilarityFunction(
        Table<TProperty> similarityTable,
        int weight = 1
        ) : base(weight)
    {
        _similarityTable = similarityTable;
    }

    internal override double Invoke<TCase>(object value, object newValue)
        where TCase : class
    {
        if(!_enumAsDictionary.TryGetValue(value.ToString(), out TProperty enumValue))
        {
            throw new ArgumentException(nameof(value));
        }

        if (!_enumAsDictionary.TryGetValue(newValue.ToString(), out TProperty enumNewValue))
        {
            throw new ArgumentException(nameof(enumNewValue));
        }

        return _similarityTable[enumValue, enumNewValue];
    }
}
