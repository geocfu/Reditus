using System;
using System.Threading;

namespace Reditus.Definitions
{
    /// <summary>
    /// Helpers that decide whether a failed operation was canceled and create the matching Error.
    /// </summary>
    internal static class OperationCanceledErrorHelpers
    {
        /// <summary>
        /// Creates an <see cref="OperationCanceledError"/> if the exception was caused by the cancellation of the
        /// provided token; otherwise, an <see cref="Error"/> holding the exception.
        /// </summary>
        /// <param name="exception">The caught exception.</param>
        /// <param name="cancellationToken">The token of the operation that threw the exception.</param>
        /// <returns>The Error that describes the exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided exception is null.</exception>
        internal static Error FromException(Exception exception, CancellationToken cancellationToken)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception), "Cannot create an Error from a null exception.");
            }

            // Only the caller's own token counts: a timeout also throws OperationCanceledException.
            if (exception is OperationCanceledException canceled && cancellationToken.IsCancellationRequested)
            {
                return new OperationCanceledError(canceled);
            }

            return new Error(exception);
        }

        /// <summary>
        /// Creates an <see cref="OperationCanceledError"/> if the provided token was canceled; otherwise, returns the provided error.
        /// </summary>
        /// <param name="error">The error to use if the token was not canceled.</param>
        /// <param name="cancellationToken">The token of the operation that failed.</param>
        /// <returns>The Error that describes the failure.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided error is null.</exception>
        internal static Error FromCancellation(Error error, CancellationToken cancellationToken)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error), "The Error property cannot be null.");
            }

            return cancellationToken.IsCancellationRequested ? new OperationCanceledError() : error;
        }
    }
}
