namespace EasyCBR.Contract.IStage;

/// <summary>
/// Represents Revise stage result.
/// </summary>
/// <typeparam name="TCase"></typeparam>
public interface IReviseStage<TCase> 
    where TCase : class
{
    /// <summary>
    /// Reians the new case.
    /// </summary>
    /// <returns></returns>
    IRetainStage<TCase> Retain();

    /// <summary>
    /// Runs the stage.
    /// </summary>
    /// <returns></returns>
    TCase Run();
}
