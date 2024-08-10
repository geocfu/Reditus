namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes a Success object.
    /// </summary>
    /// <typeparam name="T">The type containing by this.</typeparam>
    public interface ISuccess<T>
    {
        /// <summary>
        /// Gets the success of the Success Result.
        /// </summary>
        T Value { get; }
    }
}