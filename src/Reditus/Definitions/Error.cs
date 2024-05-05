using System;
using System.Collections.Generic;
using Reditus.Abstractions;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#else
using System.Collections.Immutable;
#endif

namespace Reditus.Definitions
{
    /// <summary>
    /// A general Error object.
    /// </summary>
    public class Error : IError
    {
        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; } = default;

        /// <inheritdoc />
        public IReadOnlyDictionary<string, object> Metadata => _metadata;

#if NET8_0_OR_GREATER
        private readonly FrozenDictionary<string, object> _metadata = FrozenDictionary<string, object>.Empty;
#else
        private readonly ImmutableDictionary<string, object> _metadata = ImmutableDictionary<string, object>.Empty;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="message">The message that describes the Error.</param>
        /// <exception cref="ArgumentException">Thrown if message is null or empty.</exception>
        public Error(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("The Message property must have a value.", nameof(message));
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
#if NET8_0_OR_GREATER
            _metadata = metadata.ToFrozenDictionary();
#else
            _metadata = metadata.ToImmutableDictionary();
#endif
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
#if NET8_0_OR_GREATER
            _metadata = metadata.ToFrozenDictionary();
#else
            _metadata = metadata.ToImmutableDictionary();
#endif
        }
    }
}