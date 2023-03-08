using EasyCBR.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.SimilarityFunctions
{
    public class BasicSimilarityFunction : SimilarityFunction
    {
        public BasicSimilarityFunction(int weight = 1) : base(weight) { }

        public override void Invoke<TCase>(CBR<TCase> cbr, string propertyName)
            where TCase : class
        {
            if (cbr == null)
                throw new ArgumentNullException(nameof(cbr));

            if (propertyName == null) 
                throw new ArgumentNullException(propertyName);

            if (!cbr.Properties.ContainsKey(propertyName))
                throw new ArgumentException(propertyName);

            //
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
