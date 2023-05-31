using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyCBR.Models;

public class Table<TKey> : Dictionary<(TKey, TKey), double>
    where TKey : Enum
{
    public Table() : base() 
    {
        var enumValues = Enum.GetValues(typeof(TKey)).Cast<TKey>();

        foreach (var row in enumValues)
        {
            foreach (var col in enumValues)
            {
                if (row.Equals(col))
                    this[row, col] = 1.0;

                else 
                    this[row, col] = 0.0;
            }
        }
    }

    public double this[TKey key1, TKey key2]
    {
        get
        {
            return this[(key1, key2)];
        }
        set
        {
            if (value < 0.0 || value > 1.0)
                throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0.0 and 1.0");

            this[(key1, key2)] = value;
            this[(key2, key1)] = value;
        }
    }
}