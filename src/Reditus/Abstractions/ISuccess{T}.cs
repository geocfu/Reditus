namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes a Success{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="T:Success{T}" />.</typeparam>
    public interface ISuccess<out T> : ISuccess
    {
        /// <summary>
        /// Gets the Value of the Success Result.
        /// </summary>
        T Value { get; }
    }
}