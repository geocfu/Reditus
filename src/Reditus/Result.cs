namespace Reditus;

using Definitions;
using Definitions.Abstractions;

/// <summary>
/// Represents a Result object.
/// </summary>
public class Result
{
    /// <summary>
    /// The internal state of the Result.
    /// </summary>
    internal readonly ResultState _state;

    /// <summary>
    /// The internal error, if any, of the Result.
    /// </summary>
    private readonly IError? _error;

    /// <summary>
    /// Gets a value indicating whether Result is successful.
    /// </summary>
    public bool IsSuccessful => _state is ResultState.Successful;

    /// <summary>
    /// Gets a value indicating whether Result is failed.
    /// </summary>
    public bool IsFailed => _state is ResultState.Failed;

    /// <summary>
    /// Gets the Error, if any, attached to the Result.
    /// </summary>
    public IError? Error
    {
        get
        {
            if (_state is ResultState.Successful)
            {
                throw new InvalidOperationException("Accessing the Error property of a successful Result is invalid.");
            }

            return _error;
        }

        private init
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "The Error property of a failed Result cannot be null.");
            }

            _error = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    protected Result()
    {
        _state = ResultState.Successful;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="error">The error to attach to the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    protected Result(IError error)
    {
        _state = ResultState.Failed;
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
public class Result<T> : Result
{
    /// <summary>
    /// The internal value, if any, of the successful Result.
    /// </summary>
    private readonly T? _value;

    /// <summary>
    /// Gets the value produced of the Result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
    /// <exception cref="ArgumentNullException">Thrown when Result is Successful.</exception>
    public T? Value
    {
        get
        {
            if (_state is ResultState.Failed)
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
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">The value of the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    protected Result(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="error">The error to attach to the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    protected Result(IError error)
        : base(error)
    {
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
    /// Creates a failed Result from a failed Result.
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