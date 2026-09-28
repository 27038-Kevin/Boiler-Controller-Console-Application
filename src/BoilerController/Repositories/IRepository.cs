namespace BoilerController.Repositories
{
    /// <summary>
    /// Abstract contract for a create-and-read-only repository.
    /// </summary>
    /// <typeparam name="T">Type parameter for the create-and-read-only repository.</typeparam>
    internal interface IRepository<T> where T : class, new()
    {
        /// <summary>
        /// Fetches all entities.
        /// </summary>
        /// <returns>A lazy enumerable of all entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Saves an entity to the repository.
        /// </summary>
        /// <param name="entity">The item or data model to be saved.</param>
        void Save(T entity);
    }
}
