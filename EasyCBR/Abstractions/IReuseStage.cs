namespace EasyCBR.Abstractions;

/// <summary>
/// Represents Reuse stage result.
/// </summary>
/// <typeparam name="TCase"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public interface IReuseStage<TCase, TOutput>
    where TCase : class
{
    /// <summary>
    /// Revies the value.
    /// </summary>
    /// <param name="correctValue"></param>
    /// <returns></returns>
    IReviseStage<TCase> Revise(TOutput correctValue);

    /// <summary>
    /// Confirms the value.
    /// </summary>
    /// <returns></returns>
    IReviseStage<TCase> Revise();

    /// <summary>
    /// Run the stage.
    /// </summary>
    /// <returns></returns>
    TCase Run();
}
