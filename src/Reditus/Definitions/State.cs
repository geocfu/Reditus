namespace Reditus.Definitions;

/// <summary>
/// The state of a Result.
/// </summary>
public enum State : byte
{
    /// <summary>
    /// If the Result is successful.
    /// </summary>
    Successful = 1,

    /// <summary>
    /// If the Result is failed.
    /// </summary>
    Failed = 2,
}