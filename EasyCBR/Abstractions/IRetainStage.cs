namespace EasyCBR.Abstractions;

/// <summary>
/// Represents a Retain stage result.
/// </summary>
/// <typeparam name="TCase"></typeparam>
public interface IRetainStage<TCase>
    where TCase : class
{
    /// <summary>
    /// Runs The stage
    /// </summary>
    /// <returns></returns>
    TCase Run();
}
