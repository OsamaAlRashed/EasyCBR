//using EasyCBR.Helpers;
//using EasyCBR.SimilarityFunctions.Base;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;

//namespace EasyCBR.SimilarityFunctions
//{
//    public sealed class LinearSimilarityFunction<TProperty> : SimilarityFunction
//         where TProperty : INumber<TProperty>
//    {
//        internal override int Weight { get; set; }
//        internal override List<int> Scores { get; set; }

//        public LinearSimilarityFunction(int weight = 1) : base(weight) { }

//        internal override void Invoke<TCase>(CBR<TCase> cbr, string propertyName)
//        {
//            if (cbr == null)
//                throw new ArgumentNullException(nameof(cbr));

//            if (propertyName == null)
//                throw new ArgumentNullException(propertyName);

//            if (!cbr.Properties.ContainsKey(propertyName))
//                throw new ArgumentException(propertyName);

//            if (cbr.Properties[propertyName] != typeof(Int32) && cbr.Properties[propertyName] != typeof(double)
//                && cbr.Properties[propertyName] != typeof(decimal) && cbr.Properties[propertyName] != typeof(float)
//                && cbr.Properties[propertyName] != typeof(Int16) && cbr.Properties[propertyName] != typeof(Int64))
//                throw new ArgumentException(propertyName);

//            var newCasePropertyValue = HelperMethods.GetPropertyValue(cbr.Case, propertyName);

//            for (int i = 0; i < cbr.Cases.Count; i++)
//            {
//                var value = HelperMethods.GetPropertyValue(cbr.Cases[i], propertyName);

//                Scores.Add(TProperty.CreateChecked(newCasePropertyValue) - (TProperty)value);
//            }
//        }
//    }
//}
