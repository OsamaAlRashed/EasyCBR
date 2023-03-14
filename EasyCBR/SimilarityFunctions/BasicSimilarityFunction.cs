using EasyCBR.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace EasyCBR.SimilarityFunctions
{
    public sealed class BasicSimilarityFunction<TProperty> : SimilarityFunction
        where TProperty : IEquatable<TProperty>, IComparable<TProperty>
    {
        public BasicSimilarityFunction(int weight = 1) : base(weight) { }

        internal override int Weight { get; set; }
        internal override List<int> Scores { get; set; }

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

                if (value.Equals(newCasePropertyValue))
                {
                    Scores.Add(1);
                }
                else
                {
                    Scores.Add(0);
                }
            }
        }
    }
}
