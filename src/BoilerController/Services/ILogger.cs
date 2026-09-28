namespace BoilerController.Services
{
    internal interface ILogger<T> where T : class, new()
    {
        Task Log(T entity);
    }
}
