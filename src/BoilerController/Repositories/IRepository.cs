namespace BoilerController.Repositories
{
    internal interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        void Save(T entity);
    }
}
