namespace Reditus;

using Definitions.Abstractions;

/// <summary>
/// Represents a Result object.
/// </summary>
public class Result : IResult
{
    /// <summary>
    /// Gets a value indicating whether Result is successful.
    /// </summary>
    public bool IsSuccessful => Error is null;

    /// <summary>
    /// Gets a value indicating whether Result is failed.
    /// </summary>
    public bool IsFailed => !IsSuccessful;

    /// <inheritdoc />
    public IError? Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    protected Result()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="error">The error to attach to the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    protected Result(IError error)
    {
        if (error is null)
        {
            throw new ArgumentNullException(
                nameof(error),
                "Error cannot be null. Did you mean to use the Successful() method?");
        }

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
/// Represents a Result{T} object.
/// </summary>
/// <typeparam name="T">The type of the Result.</typeparam>
public class Result<T> : IResult<T>
{
    /// <summary>
    /// The value, if any, of the Result.
    /// </summary>
    private readonly T? _value;

    private readonly IError? _error;

    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
    /// <exception cref="ArgumentNullException">Thrown when Result is Successful.</exception>
    public T? Value
    {
        get
        {
            if (Error is not null)
            {
                throw new InvalidOperationException("Trying to access the Value of a failed Result is not valid.");
            }

            return _value;
        }

        private init
        {
            if (Error is not null)
            {
                throw new InvalidOperationException("Trying to access the Value of a failed Result is not valid.");
            }

            if (value is null)
            {
                throw new ArgumentNullException(
                    nameof(value),
                    "Value cannot be null. Did you mean to use the Result.Successful() method instead?");
            }

            _value = value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether Result is successful.
    /// </summary>
    public bool IsSuccessful => Error is null;

    /// <summary>
    /// Gets a value indicating whether Result is failed.
    /// </summary>
    public bool IsFailed => !IsSuccessful;

    /// <inheritdoc />
    public IError? Error
    {
        get
        {
            if (Value is not null)
            {
                throw new InvalidOperationException("Trying to access the Error of a successful Result is not valid.");
            }

            return _error;
        }

        private init
        {
            if (Value is not null)
            {
                throw new InvalidOperationException("Trying to access the Error of a successful Result is not valid.");
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "The Error property of a failed Result cannot be null.");
            }

            _error = value;
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
    {
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
}