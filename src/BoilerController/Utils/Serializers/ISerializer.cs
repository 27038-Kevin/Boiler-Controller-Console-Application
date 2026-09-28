namespace BoilerController.Utils.Serializers
{
    internal interface ISerializer<T> where T : class, new()
    {
        string Serialize(T entity);

        T Deserialize(string line);
    }
}
