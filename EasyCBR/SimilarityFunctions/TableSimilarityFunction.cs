using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyCBR.SimilarityFunctions;

/// <summary>
/// Represnts Table similarity function.
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public sealed class TableSimilarityFunction<TProperty> : SimilarityFunction
    where TProperty : Enum
{
    private double[,] _similarityMatrix;
    internal override int Weight { get; set; }
    internal override List<double> Scores { get; set; }

    internal Dictionary<object, int> EnumIndexes = Enum.GetValues(typeof(TProperty)).Cast<object>()
        .Select((value, i) => new { index = i, value = value })
        .ToDictionary(x => x.value, x => x.index);

    public TableSimilarityFunction(int weight = 1) : base(weight)
    {
        Init();
    }

    public TableSimilarityFunction(double[,] matrix, int weight = 1) : base(weight)
    {
        Init();
        _similarityMatrix = matrix;
    }

    private void Init()
    {
        _similarityMatrix = new double[EnumIndexes.Count, EnumIndexes.Count];

        for (int i = 0; i < EnumIndexes.Count; i++)
        {
            for (int j = 0; j < EnumIndexes.Count; j++)
            {
                if (i == j)
                    _similarityMatrix[i, j] = 1.0;
                else
                    _similarityMatrix[i, j] = 0.0;
            }
        }
    }

    internal override double Invoke<TCase>(object value, object newValue)
        where TCase : class
    {
        if(!EnumIndexes.TryGetValue(value, out int index))
        {
            throw new ArgumentException(nameof(value));
        }

        if (!EnumIndexes.TryGetValue(newValue, out int newIndex))
        {
            throw new ArgumentException(nameof(newValue));
        }

        return _similarityMatrix[index, newIndex];
    }
}
