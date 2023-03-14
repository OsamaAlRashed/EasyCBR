using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.SimilarityFunctions
{
    public abstract class SimilarityFunction
    {
        public SimilarityFunction(int weight = 1)
        {
            Weight = weight;
            Scores = new List<int>();
        }

        internal abstract int Weight { get; set; }
        internal abstract List<int> Scores { get; set; }

        internal abstract void Invoke<TCase>(CBR<TCase> cbr, string propertyName) where TCase : class;
    }
}
