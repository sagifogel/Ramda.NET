namespace Ramda.NET
{
    /// <summary>
    /// Provides a contract to interpose a seperator between elements
    /// </summary>
    /// <typeparam name="TSeperator">The type of the seperator.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IDispersible<TSeperator, TResult>
    {
        /// <summary>
        /// Intersperses the specified seperator.
        /// </summary>
        /// <param name="seperator">The seperator.</param>
        /// <returns></returns>
        TResult Intersperse(TSeperator seperator);
    }
}
