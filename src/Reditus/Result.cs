namespace Reditus;

using Definitions;
using Definitions.Abstractions;

/// <summary>
/// Represents a Result object.
/// </summary>
public sealed class Result
{
    /// <summary>
    /// The internal state of the Result.
    /// </summary>
    private readonly State _state;

    /// <summary>
    /// The internal error, if any, of the Result.
    /// </summary>
    private readonly IError? _error;

    /// <summary>
    /// Gets a value indicating whether Result is successful.
    /// </summary>
    public bool IsSuccessful => _state is State.Successful;

    /// <summary>
    /// Gets a value indicating whether Result is failed.
    /// </summary>
    public bool IsFailed => _state is State.Failed;

    /// <summary>
    /// Gets the Error, if any, attached to the Result.
    /// </summary>
    public IError? Error
    {
        get
        {
            if (_state is State.Successful)
            {
                throw new InvalidOperationException("Accessing the Error property of a successful Result is invalid.");
            }

            return _error;
        }

        private init => _error = value ?? throw new ArgumentNullException(
            nameof(value),
            "The Error property of a failed Result cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    private Result()
    {
        _state = State.Successful;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="error">The error to attach to the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    private Result(IError error)
    {
        _state = State.Failed;
        Error = error;
    }

    /// <summary>
    /// Creates a successful Result.
    /// </summary>
    /// <returns>A successful Result instance.</returns>
    public static Result Successful() => new();

    /// <summary>
    /// Creates a failed Result.
    /// </summary>
    /// <param name="error">The error of the result.</param>
    /// <returns>A failed Result instance.</returns>
    public static Result Failed(IError error) => new(error);
}

/// <summary>
/// Represents a Result object that can return a value.
/// </summary>
/// <typeparam name="T">The type produced by this <see cref="T:Result{T}" />.</typeparam>
public sealed class Result<T>
{
    /// <summary>
    /// The internal state of the Result.
    /// </summary>
    private readonly State _state;

    /// <summary>
    /// The internal value, if any, of the Result.
    /// </summary>
    private readonly T? _value;

    /// <summary>
    /// The internal error, if any, of the Result.
    /// </summary>
    private readonly IError? _error;

    /// <summary>
    /// Gets a value indicating whether Result is successful.
    /// </summary>
    public bool IsSuccessful => _state is State.Successful;

    /// <summary>
    /// Gets a value indicating whether Result is failed.
    /// </summary>
    public bool IsFailed => _state is State.Failed;

    /// <summary>
    /// Gets the value produced of the Result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
    /// <exception cref="ArgumentNullException">Thrown when Result is Successful.</exception>
    public T? Value
    {
        get
        {
            if (_state is State.Failed)
            {
                throw new InvalidOperationException("Accessing the Value property of a failed Result is invalid.");
            }

            return _value;
        }

        private init
        {
            if (value is null)
            {
                throw new ArgumentNullException(
                    nameof(value),
                    "The Value property of a successful Result cannot be null. Did you mean to use the Result.Successful() method instead?");
            }

            _value = value;
        }
    }

    /// <summary>
    /// Gets the Error, if any, attached to the Result.
    /// </summary>
    public IError? Error
    {
        get
        {
            if (_state is State.Successful)
            {
                throw new InvalidOperationException("Accessing the Error property of a successful Result is invalid.");
            }

            return _error;
        }

        private init => _error = value ?? throw new ArgumentNullException(
            nameof(value),
            "The Error property of a failed Result cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">The value of the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    private Result(T value)
    {
        _state = State.Successful;
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="error">The error to attach to the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    private Result(IError error)
    {
        _state = State.Failed;
        Error = error;
    }

    /// <summary>
    /// Creates a successful Result.
    /// </summary>
    /// <param name="value">The value of the Result.</param>
    /// <returns>A successful Result instance.</returns>
    public static Result<T> Successful(T value) => new(value);

    /// <summary>
    /// Creates a failed Result.
    /// </summary>
    /// <param name="error">The error of the result.</param>
    /// <returns>A failed Result instance.</returns>
    public static Result<T> Failed(IError error) => new(error);

    /// <summary>
    /// Creates a failed Result.
    /// </summary>
    /// <param name="result">The Result to copy from.</param>
    /// <returns>A failed Result instance.</returns>
    /// <typeparam name="TY">The type of the Result to copy from.</typeparam>
    public static Result<T> Failed<TY>(Result<TY> result)
    {
        if (result.IsSuccessful)
        {
            throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
        }

        return new Result<T>(result.Error!);
    }
}