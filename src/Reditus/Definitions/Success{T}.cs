using System;
using Reditus.Abstractions;

namespace Reditus.Definitions
{
    /// <summary>
    /// Represents a Success object that encapsulates a value of type <typeparamref name="T"/>.
    /// Implements the <see cref="ISuccess{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Success{T}"/> class.</typeparam>
    public class Success<T> : ISuccess<T>
    {
        /// <summary>
        /// Backing field for the Value property.
        /// </summary>
        private T _value;

        /// <inheritdoc />
        public T Value
        {
            get
            {
                return _value;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The Value property must have a value.");
                }

                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success{T}"/> class with the specified value.
        /// </summary>
        /// <param name="value">The value to initialize the Success object with.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided value is null.</exception>
        public Success(T value)
        {
            Value = value;
        }
    }
}