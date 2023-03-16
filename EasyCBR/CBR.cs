using EasyCBR.Attributes;
using EasyCBR.Contract.IStage;
using EasyCBR.Enums;
using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EasyCBR.Helpers.HelperMethods;
namespace EasyCBR
{
    public sealed class CBR<TCase> :
        IRetriveStage<TCase>,
        IRetainStage<TCase>,
        IReuseStage<TCase>,
        IReviseStage<TCase>
        where TCase : class
    {

        #region Properties
        internal TCase Case { get; set; } = Activator.CreateInstance<TCase>();

        internal (TCase, double) ResultCase { get; set; } = Activator.CreateInstance<(TCase, double)>();

        internal List<TCase> Cases { get; set; } = new List<TCase>();

        internal List<(TCase, double)> SelectedCases { get; set; } = new List<(TCase, double)>();

        internal Dictionary<string, Type> Properties { get; set; } = new Dictionary<string, Type>();

        internal ValueTuple<string, Type> TargetProperty { get; set; }

        internal Dictionary<string, SimilarityFunction> SimilarityFunctionsPerProperties { get; set; }
            = new Dictionary<string, SimilarityFunction>();
        #endregion

        #region Initlize
        private CBR() { }   

        //TODO public
        private static CBR<TCase> Create(string path)
        {
            var cbr = new CBR<TCase>();

            /// ToDo
            Init(cbr, new List<TCase>());

            return cbr;
        }

        public static CBR<TCase> Create(List<TCase> cases)
        {
            var cbr = new CBR<TCase>();

            Init(cbr, cases);

            return cbr;
        }

        private static void Init(CBR<TCase> cbr, List<TCase> cases)
        {
            cbr.Properties = GetNameAndTypeProperties<TCase>();
            cbr.Cases = cases;
            cbr.TargetProperty = GetPropertyHasCustomAttribute<TCase, OutputAttribute>();

            cbr.Properties = cbr.Properties.Where(x => x.Key != cbr.TargetProperty.Item1)
                .ToDictionary(x => x.Key, y => y.Value);
        }
        #endregion

        #region 4R
        public IRetainStage<TCase> Retain()
        {
            ///TODO
            return this;
        }

        public IRetriveStage<TCase> Retrieve(TCase Case, int count)
        {
            if (count <= 0 || Cases.Count < count)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.Case = Case;
            SelectedCases = InvokeAllSimilarityFunctions().Take(count).ToList();

            return this;
        }

        public IReuseStage<TCase> Reuse(ChooseType chooseType = ChooseType.MaxSimilarity)
        {
            var resultCaseValue = SelectedCases
                .OrderByDescending(selectedCase => selectedCase.Item1.GetType()
                .GetProperties().Where(x => x.Name == TargetProperty.Item1 && x.PropertyType == TargetProperty.Item2)
                .FirstOrDefault().GetValue(selectedCase.Item1, null)).ToList();

            //ToDo
            ResultCase = chooseType switch
            {
                ChooseType.MaxSimilarity => SelectedCases.FirstOrDefault(),
                ChooseType.AverageSimilarity => SelectedCases.Skip(SelectedCases.Count / 2).FirstOrDefault(),
                ChooseType.MinSimilarity => SelectedCases.LastOrDefault(),
                ChooseType.MaxValue => resultCaseValue.FirstOrDefault(),
                ChooseType.AverageValue => resultCaseValue.Skip(SelectedCases.Count / 2).FirstOrDefault(),
                ChooseType.MinValue => resultCaseValue.LastOrDefault(),
                _ => throw new ArgumentOutOfRangeException(nameof(chooseType)),
            };

            return this;
        }

        public IReviseStage<TCase> Revise(object correctValue)
        {
            ///TODO
            if (correctValue.GetType() != TargetProperty.Item2)
                throw new ArgumentException();

            return this;
        }
        #endregion

        #region Similarity
        public CBR<TCase> SetSimilarityFunctions(params (string property, SimilarityFunction similarityFunction)[] pairs)
        {
            foreach (var (property, similarityFunction) in pairs)
            {
                SimilarityFunctionsPerProperties.TryAdd(property, similarityFunction);
            }

            return this;
        }

        private List<(TCase, double)> InvokeAllSimilarityFunctions()
        {
            foreach (var pair in SimilarityFunctionsPerProperties)
            {
                pair.Value.Invoke(this, pair.Key);
            }

            List<double> totalScores = new List<double>();

            for (int i = 0; i < Cases.Count; i++)
            {
                totalScores.Add(0);
                foreach (var pair in SimilarityFunctionsPerProperties)
                {
                    totalScores[i] += pair.Value.Scores[i] * pair.Value.Weight;
                }
            }

            return Cases.Zip(totalScores, (first, second) => new ValueTuple<TCase, double>(first, second))
                .OrderByDescending(x => x.Item2).ToList();
        }
        #endregion

        #region Run Methods

        List<TCase> IRetriveStage<TCase>.Run() => SelectedCases.Select(x => x.Item1).ToList();
        
        TCase IRetainStage<TCase>.Run() => ResultCase.Item1;

        TCase IReuseStage<TCase>.Run() => ResultCase.Item1;

        TCase IReviseStage<TCase>.Run() => ResultCase.Item1;

        #endregion

    }
}
