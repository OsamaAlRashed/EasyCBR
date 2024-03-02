using EasyCBR.Enums;
using System.Collections.Generic;

namespace EasyCBR.Abstractions;

/// <summary>
/// Represents Retrieve stage result.
/// </summary>
/// <typeparam name="TCase"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public interface IRetrieveStage<TCase, TOutput>
    where TCase : class
{
    /// <summary>
    /// Reuses the selected case.
    /// </summary>
    /// <param name="selectType">Chooses the way to select case</param>
    /// <returns></returns>
    IReuseStage<TCase, TOutput> Reuse(SelectType selectType = SelectType.MaxSimilarity);

    /// <summary>
    /// Runs the stage
    /// </summary>
    /// <returns></returns>
    List<TCase> Run();
}
