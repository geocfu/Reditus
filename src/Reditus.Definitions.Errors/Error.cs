namespace Reditus.Definitions.Errors;

using System.Collections.Frozen;
using Abstractions;

/// <summary>
/// A general Error object.
/// </summary>
public class Error : IError
{
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
    /// <exception cref="ArgumentException">Thrown if message is null or empty.</exception>
    public Error(string message, Dictionary<string, object> metadata) : this(message)
    {
        Metadata = metadata.ToFrozenDictionary();
    }
    
    public Error(string message, Exception exception) : this(message)
    {
        Exception = exception;
    }
    
    public Error(string message, Exception exception, Dictionary<string, object> metadata) : this(message, exception)
    {
        Metadata = metadata.ToFrozenDictionary();
    }
    
    /// <inheritdoc />
    public string Message { get; }
    
    /// <inheritdoc />
    public Exception? Exception { get; }
    
    /// <inheritdoc />
    public FrozenDictionary<string, object> Metadata { get; } = FrozenDictionary<string, object>.Empty;
}