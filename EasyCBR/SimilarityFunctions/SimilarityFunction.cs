using System;
using System.Collections.Generic;
using System.Text;
using EasyCBR.Contract;

namespace EasyCBR.SimilarityFunctions
{
    public abstract class SimilarityFunction : ISimilarityFunction
    {
        private int _weight;

        public SimilarityFunction(int weight = 1)
        {
            _weight = weight;
        }
    }
}
