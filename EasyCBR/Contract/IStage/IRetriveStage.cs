using EasyCBR.Enums;
using System.Collections.Generic;

namespace EasyCBR.Contract.IStage;

/// <summary>
/// Represents Retrive stage result.
/// </summary>
/// <typeparam name="TCase"></typeparam>
public interface IRetriveStage<TCase>
    where TCase : class
{
    /// <summary>
    /// Reuses the selected case.
    /// </summary>
    /// <param name="chooseType">Chooses the way to select case</param>
    /// <returns></returns>
    IReuseStage<TCase> Reuse(SelectType selectType = SelectType.MaxSimilarity);

    /// <summary>
    /// Runs the stage
    /// </summary>
    /// <returns></returns>
    List<TCase> Run();
}
