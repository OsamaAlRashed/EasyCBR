//using EasyCBR.Helpers;
//using EasyCBR.SimilarityFunctions.Base;
//using System;
//using System.Collections.Generic;
//using System.Numerics;
//using System.Text;

//namespace EasyCBR.SimilarityFunctions;

//public sealed class ScalingSimilarityFunction<TProperty> : SimilarityFunction
//     where TProperty : INumber<TProperty>
//{
//    private TProperty _min;
//    private TProperty _max;

//    internal override int Weight { get; set; }
//    internal override List<double> Scores { get; set; }

//    public ScalingSimilarityFunction(int weight = 1) : base(weight) { }

//    public ScalingSimilarityFunction(int min, int max, int weight = 1) : base(weight)
//    {
//        _min = min;
//        _max = max;
//    }

//    internal override double Invoke<TCase>(object value, object newValue)
//    {
//        TProperty range = _min - _max + TProperty.CreateChecked(1);
//        return Math.Round(
//                (TProperty.CreateChecked(1) / range)
//              * (range - Math.Abs(TProperty.Parse(value - newValue, System.Globalization.NumberStyles.Number)))
//            , 2);
//    }
//}
