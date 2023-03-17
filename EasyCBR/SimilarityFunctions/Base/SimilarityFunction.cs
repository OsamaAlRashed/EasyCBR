using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.SimilarityFunctions.Base
{
    public abstract class SimilarityFunction
    {
        public SimilarityFunction(int weight = 1)
        {
            Weight = weight;
            Scores = new List<double>();
        }

        internal abstract int Weight { get; set; }
        internal abstract List<double> Scores { get; set; }

        internal abstract void Invoke<TCase>(CBR<TCase> cbr, string propertyName) where TCase : class;
    }
}
