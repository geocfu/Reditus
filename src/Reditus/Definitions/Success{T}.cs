using System;
using Reditus.Abstractions;

namespace Reditus.Definitions
{
    /// <summary>
    /// Represents a Success{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="T:Success{T}" />.</typeparam>
    public class Success<T> : ISuccess<T>
    {
        /// <inheritdoc />
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success{T}"/> class.
        /// </summary>
        /// <param name="value">The message that describes the Failure.</param>
        /// <exception cref="ArgumentNullException">Thrown if value is null.</exception>
        public Success(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "The Value property must have a value.");
            }

            Value = value;
        }
    }
}