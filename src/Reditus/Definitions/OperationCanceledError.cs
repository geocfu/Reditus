namespace Reditus.Definitions
{
    /// <summary>
    /// Represents an Error caused by a canceled operation, for example a cancellation requested through a
    /// <see cref="System.Threading.CancellationToken"/>. Lets callers tell a cancellation apart from a failure.
    /// </summary>
    public class OperationCanceledError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCanceledError"/> class.
        /// The Exception property will be initialized to null.
        /// </summary>
        public OperationCanceledError()
            : base("The operation was canceled.")
        {
        }
    }
}
