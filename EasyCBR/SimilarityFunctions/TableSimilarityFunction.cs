using EasyCBR.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.SimilarityFunctions
{
    public sealed class TableSimilarityFunction<TProperty> : SimilarityFunction
        where TProperty : Enum
    {
        private double[,] _similarityMatrix;
        internal override int Weight { get; set; }
        internal override List<int> Scores { get; set; }

        public TableSimilarityFunction(int weight = 1) : base(weight)
        {
            var enumValues = Enum.GetValues(typeof(TProperty));
            Init(enumValues);
        }

        public TableSimilarityFunction(double [,] matrix, int weight = 1) : base(weight)
        {
            var enumValues = Enum.GetValues(typeof(TProperty));
            Init(enumValues);

        }

        private void Init(Array enumValues)
        {
            _similarityMatrix = new double[enumValues.Length, enumValues.Length];

            for (int i = 0; i < enumValues.Length; i++)
            {
                for (int j = 0; j < enumValues.Length; j++)
                {
                    if (i == j)
                        _similarityMatrix[i, j] = 1.0;
                    else
                        _similarityMatrix[i, j] = 0.0;
                }
            }
        }

        internal override void Invoke<TCase>(CBR<TCase> cbr, string propertyName)
            where TCase : class
        {
            if (cbr == null)
                throw new ArgumentNullException(nameof(cbr));

            if (propertyName == null)
                throw new ArgumentNullException(propertyName);

            if (!cbr.Properties.ContainsKey(propertyName))
                throw new ArgumentException(propertyName);

            if (typeof(TProperty) != cbr.Properties[propertyName])
                throw new ArgumentException(propertyName);

            var newCasePropertyValue = HelperMethods.GetPropertyValue(cbr.Case, propertyName);

            for (int i = 0; i < cbr.Cases.Count; i++)
            {
                var value = HelperMethods.GetPropertyValue(cbr.Cases[i], propertyName);

                
            }
        }
    }
}
