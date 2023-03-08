using System;
using System.Collections.Generic;
using System.Text;
using EasyCBR.Contract;

namespace EasyCBR.SimilarityFunctions
{
    public abstract class SimilarityFunction : ISimilarityFunction
    {
        public int Weight { get; set; }
        public List<int> Scores { get; set; }

        public SimilarityFunction(int weight = 1)
        {
            Weight = weight;
            Scores = new List<int>();
        }

        public abstract void Invoke<TCase>(CBR<TCase> cbr, string propertyName) where TCase : class;
    }
}
