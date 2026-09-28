namespace BoilerController.Services
{
    internal interface ILogger<T> where T : class, new()
    {
        void Log(T entity);
    }
}
