namespace Reditus.Abstractions;

using System.Collections.Frozen;

/// <summary>
/// Represents an error object.
/// </summary>
public interface IError
{
    /// <summary>
    /// Gets the message that describes the Error.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the exception, if any, that was attached to the Error.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Gets the metadata, if any, that was attached to the Error.
    /// </summary>
    public FrozenDictionary<string, object> Metadata { get; }
}