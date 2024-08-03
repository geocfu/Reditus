using System;
using Reditus.Abstractions;

namespace Reditus.Definitions
{
    /// <summary>
    /// A general Success object.
    /// </summary>
    /// <typeparam name="T">The type of the Success object.</typeparam>
    public class Success<T> : ISuccess<T>
    {
        /// <inheritdoc />
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success{T}"/> class.
        /// </summary>
        /// <param name="value">The message that describes the Failure.</param>
        /// <exception cref="ArgumentException">Thrown if message is null or empty.</exception>
        public Success(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("The Success property must have a success.", nameof(value));
            }

            Value = value;
        }
    }
}