namespace Reditus.Definitions;

using System.Collections.Frozen;
using Abstractions;

/// <summary>
/// A general Error object.
/// </summary>
public class Error : IError
{
    /// <inheritdoc />
    public string Message { get; }

    /// <inheritdoc />
    public Exception? Exception { get; }

    /// <inheritdoc />
    public FrozenDictionary<string, object> Metadata { get; } = FrozenDictionary<string, object>.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="message">The message that describes the Error.</param>
    /// <exception cref="ArgumentException">Thrown if message is null or empty.</exception>
    public Error(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("The property Message must have a value.", nameof(message));
        }

        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="message">The message that describes the Error.</param>
    /// <param name="metadata">The metadata that enriches the Error.</param>
    public Error(string message, Dictionary<string, object> metadata)
        : this(message)
    {
        Metadata = metadata.ToFrozenDictionary();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="message">The message that describes the Error.</param>
    /// <param name="exception">The exception to attach to the Error.</param>
    public Error(string message, Exception exception)
        : this(message)
    {
        Exception = exception;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="message">The message that describes the Error.</param>
    /// <param name="exception">The exception to attach to the Error.</param>
    /// <param name="metadata">The metadata that enriches the Error.</param>
    public Error(string message, Exception exception, Dictionary<string, object> metadata)
        : this(message, exception)
    {
        Metadata = metadata.ToFrozenDictionary();
    }
}