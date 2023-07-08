using EasyCBR.Abstractions;
using EasyCBR.Enums;
using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static EasyCBR.Helpers.HelperMethods;

namespace EasyCBR;

public sealed class CBR<TCase, TOutput> :
    IRetriveStage<TCase, TOutput>,
    IRetainStage<TCase>,
    IReuseStage<TCase, TOutput>,
    IReviseStage<TCase>
    where TCase : class
{

    #region Properties
    public TCase Case { get; set; } = Activator.CreateInstance<TCase>();

    public ResultCase<TCase> ResultCase { get; set; } = new();

    public List<TCase> Cases { get; set; } = new List<TCase>();

    public List<ResultCase<TCase>> SelectedCases { get; set; } = new();

    public string FilePath { get; set; } = null;

    internal Dictionary<string, Type> Properties { get; set; } = new Dictionary<string, Type>();

    internal TargetProperty TargetProperty { get; set; } = new();

    internal Dictionary<string, SimilarityFunction> SimilarityFunctionsPerProperties { get; set; }
        = new Dictionary<string, SimilarityFunction>();
    #endregion

    #region Initlize
    private CBR() { }   

    public static CBR<TCase, TOutput> Create(List<TCase> cases)
    {
        if(cases == null) 
            throw new ArgumentNullException(nameof(cases));

        if(cases.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(cases));

        var cbr = new CBR<TCase, TOutput>();

        Init(cbr, cases);

        return cbr;
    }

    public static void Init(CBR<TCase, TOutput> cbr, List<TCase> cases)
    {
        cbr.Properties = GetNameAndTypeProperties<TCase>();
        cbr.Cases = cases;

        cbr.Properties = cbr.Properties.Where(x => x.Key != cbr.TargetProperty.Name)
            .ToDictionary(x => x.Key, y => y.Value);
    }
    #endregion

    #region 4R
    public IRetriveStage<TCase, TOutput> Retrieve(TCase @case, int count)
    {
        if (count <= 0 || Cases.Count < count)
            throw new ArgumentOutOfRangeException(nameof(count));

        this.Case = @case ?? throw new ArgumentNullException(nameof(@case));
        SelectedCases = InvokeAllSimilarityFunctions().Take(count).ToList();

        return this;
    }
    
    public IReuseStage<TCase, TOutput> Reuse(SelectType chooseType = SelectType.MaxSimilarity)
    {
        var resultCaseValue = SelectedCases
            .OrderByDescending(selectedCase => 
                selectedCase.Case
                .GetType()
                .GetProperties()
                .Where(x => x.Name == TargetProperty.Name && x.PropertyType == TargetProperty.Type)
                .FirstOrDefault()
                .GetValue(selectedCase.Case, null))
            .ToList();

        ResultCase = chooseType switch
        {
            SelectType.MaxSimilarity => SelectedCases.FirstOrDefault(),
            SelectType.AverageSimilarity => SelectedCases.Skip(SelectedCases.Count / 2).FirstOrDefault(),
            SelectType.MinSimilarity => SelectedCases.LastOrDefault(),
            SelectType.MaxValue => resultCaseValue.FirstOrDefault(),
            SelectType.AverageValue => resultCaseValue.Skip(SelectedCases.Count / 2).FirstOrDefault(),
            SelectType.MinValue => resultCaseValue.LastOrDefault(),
            _ => throw new ArgumentOutOfRangeException(nameof(chooseType)),
        };

        return this;
    }

    public IReviseStage<TCase> Revise(TOutput correctValue)
    {
        PropertyInfo propInfo = ResultCase.Case.GetType().GetProperty(TargetProperty.Name);
        propInfo.SetValue(ResultCase.Case, correctValue);

        return this;
    }

    public IReviseStage<TCase> Revise() => this;

    public IRetainStage<TCase> Retain()
    {
        Cases.Add(ResultCase.Case);
        return this;
    }
    #endregion

    #region Similarity
    private List<ResultCase<TCase>> InvokeAllSimilarityFunctions()
    {
        foreach (var pair in SimilarityFunctionsPerProperties)
        {
            pair.Value.InvokeCore(this, pair.Key);
        }

        List<double> totalScores = new();

        for (int i = 0; i < Cases.Count; i++)
        {
            totalScores.Add(0);
            foreach (var pair in SimilarityFunctionsPerProperties)
            {
                totalScores[i] += pair.Value.Scores[i] * pair.Value.Weight;
            }
        }

        return Cases.Zip(totalScores, (first, second) => new ResultCase<TCase>()
        {
            Case = first,
            Result = second
        }).OrderByDescending(x => x.Result).ToList();
    }
    #endregion

    #region Run Methods

    List<TCase> IRetriveStage<TCase, TOutput>.Run() => SelectedCases.Select(x => x.Case).ToList();
    
    TCase IRetainStage<TCase>.Run() => ResultCase.Case;

    TCase IReuseStage<TCase, TOutput>.Run() => ResultCase.Case;

    TCase IReviseStage<TCase>.Run() => ResultCase.Case;

    #endregion

}
