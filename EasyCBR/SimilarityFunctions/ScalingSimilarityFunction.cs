using EasyCBR.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace EasyCBR.SimilarityFunctions
{
    public sealed class ScalingSimilarityFunction<TProperty> : SimilarityFunction
         where TProperty : INumber<TProperty>
    {
        private int _min;
        private int _max;

        internal override int Weight { get; set; }
        internal override List<int> Scores { get; set; }

        public ScalingSimilarityFunction(int weight = 1) : base(weight) { }

        public ScalingSimilarityFunction(int min, int max, int weight = 1) : base(weight) 
        { 
            _min = min;
            _max = max;
        }

        internal override void Invoke<TCase>(CBR<TCase> cbr, string propertyName)
        {
            if (cbr == null)
                throw new ArgumentNullException(nameof(cbr));

            if (propertyName == null)
                throw new ArgumentNullException(propertyName);

            if (!cbr.Properties.ContainsKey(propertyName))
                throw new ArgumentException(propertyName);

            if (cbr.Properties[propertyName] != typeof(Int32) && cbr.Properties[propertyName] != typeof(double)
                && cbr.Properties[propertyName] != typeof(decimal) && cbr.Properties[propertyName] != typeof(float)
                && cbr.Properties[propertyName] != typeof(Int16) && cbr.Properties[propertyName] != typeof(Int64))
                throw new ArgumentException(propertyName);

            var newCasePropertyValue = HelperMethods.GetPropertyValue(cbr.Case, propertyName);

            for (int i = 0; i < cbr.Cases.Count; i++)
            {
                var value = HelperMethods.GetPropertyValue(cbr.Cases[i], propertyName);

                //GetLinearValue(_min, _max, value, newCasePropertyValue)
            }
        }

        //private TProperty GetLinearValue(TProperty min, TProperty max, TProperty val, TProperty newval)
        //{
        //    TProperty range = max - min + TProperty.CreateChecked(1);
        //    return 
        //        Math.Round(
        //            (TProperty.CreateChecked(1) / range) 
        //          * (range - Math.Abs(TProperty.Parse(val - newval, System.Globalization.NumberStyles.Number)))
        //        , 2);
        //}
    }
}
