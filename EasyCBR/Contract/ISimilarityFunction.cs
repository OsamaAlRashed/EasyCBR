using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract
{
    public interface ISimilarityFunction
    {
        public int Weight { get; set; }
        public List<int> Scores { get; set; }

        void Invoke<TCase>(CBR<TCase> cbr, string propertyName) where TCase : class;
    }
}
