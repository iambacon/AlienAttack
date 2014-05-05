using System.Collections.Generic;

namespace AlienAttack.Web.Proxy
{
    /// <summary>
    /// The interface for the space probe proxy.
    /// </summary>
    public interface ISpaceProbeProxy
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        IEnumerable<string> GetData(string email);

        /// <summary>
        /// Submits the data.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        string SubmitData(string email, Coordinate position);
    }
}
