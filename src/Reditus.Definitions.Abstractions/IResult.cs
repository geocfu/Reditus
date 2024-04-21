namespace Reditus.Definitions.Abstractions;

using System.Collections.Immutable;

/// <summary>
/// Describes a <see cref="Result"/> object.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether Result is successful.
    /// </summary>
    public bool IsSuccessful { get; }

    /// <summary>
    /// Gets a value indicating whether Result is failed.
    /// </summary>
    public bool IsFailed { get; }

    /// <summary>
    /// Gets the Error, if any, attached to the Result.
    /// </summary>
    public IError? Error { get; }
}

/// <summary>
/// Describes a <see cref="Result{T}"/> object.
/// </summary>
/// <typeparam name="T">The type of the Result.</typeparam>
public interface IResult<out T> : IResult
{
    /// <summary>
    /// Gets the value of the Result.
    /// </summary>
    public T? Value { get; }
}