namespace BoilerController.Services
{
    /// <summary>
    /// Abstract contract for behavior characterizing a logger class.
    /// </summary>
    /// <typeparam name="T">The entity/data model type to be logged.</typeparam>
    internal interface ILogger<T> where T : class, new()
    {
        /// <summary>
        /// Logs an entity.
        /// </summary>
        /// <param name="entity">The data model to be logged.</param>
        void Log(T entity);

        /// <summary>
        /// Reads the logs and returns results.
        /// </summary>
        /// <returns>A lazy enumerable containing logs.</returns>
        IEnumerable<T> ReadLogs();
    }
}
