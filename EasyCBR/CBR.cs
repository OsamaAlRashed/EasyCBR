using EasyCBR.Contract.IStage;
using EasyCBR.Enums;
using EasyCBR.Models;
using EasyCBR.SimilarityFunctions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static EasyCBR.Helpers.HelperMethods;
namespace EasyCBR;

public sealed class CBR<TCase> :
    IRetriveStage<TCase>,
    IRetainStage<TCase>,
    IReuseStage<TCase>,
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

    public static CBR<TCase> Create(List<TCase> cases)
    {
        var cbr = new CBR<TCase>();

        Init(cbr, cases);

        return cbr;
    }

    public static void Init(CBR<TCase> cbr, List<TCase> cases)
    {
        cbr.Properties = GetNameAndTypeProperties<TCase>();
        cbr.Cases = cases;

        cbr.Properties = cbr.Properties.Where(x => x.Key != cbr.TargetProperty.Name)
            .ToDictionary(x => x.Key, y => y.Value);
    }
    #endregion

    #region 4R
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
            .OrderByDescending(selectedCase => 
                selectedCase.Case
                .GetType()
                .GetProperties()
                .Where(x => x.Name == TargetProperty.Name && x.PropertyType == TargetProperty.Type)
                .FirstOrDefault()
                .GetValue(selectedCase.Case, null)
                ).ToList();

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
        if (correctValue.GetType() != TargetProperty.Type)
            throw new ArgumentException();

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

        return Cases.Zip(totalScores, (first, second) => new ResultCase<TCase>()
        {
            Case = first,
            Result = second
        }).OrderByDescending(x => x.Result).ToList();
    }
    #endregion

    #region Run Methods

    List<TCase> IRetriveStage<TCase>.Run() => SelectedCases.Select(x => x.Case).ToList();
    
    TCase IRetainStage<TCase>.Run() => ResultCase.Case;

    TCase IReuseStage<TCase>.Run() => ResultCase.Case;

    TCase IReviseStage<TCase>.Run() => ResultCase.Case;

    #endregion

}
