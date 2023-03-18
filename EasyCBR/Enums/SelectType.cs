namespace EasyCBR.Enums;

/// <summary>
/// Select the way to reuse the case.
/// </summary>
public enum SelectType
{
    /// <summary>
    /// Select the case that has max similarity.
    /// </summary>
    MaxSimilarity,
    
    /// <summary>
    /// Select the case that has min similarity.
    /// </summary>
    MinSimilarity,
    
    /// <summary>
    /// Select the case that has average similarity.
    /// </summary>
    AverageSimilarity,

    /// <summary>
    /// Select the case that has min value.
    /// </summary>
    MinValue,

    /// <summary>
    /// Select the case that has max value.
    /// </summary>
    MaxValue,

    /// <summary>
    /// Select the case that has average value.
    /// </summary>
    AverageValue
}
