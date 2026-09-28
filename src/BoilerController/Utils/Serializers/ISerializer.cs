namespace BoilerController.Utils.Serializers
{
    /// <summary>
    /// Defines an abstract contract for serializing data from one form to another.
    /// </summary>
    /// <typeparam name="T">The type of object/data to be serialized/deserialized.</typeparam>
    internal interface ISerializer<T> where T : class, new()
    {
        /// <summary>
        /// Serializes an entity into a string of text.
        /// </summary>
        /// <param name="entity">The data model or object that will undergo serialization.</param>
        /// <returns>The string representing the context of the entity.</returns>
        string Serialize(T entity);

        /// <summary>
        /// Deserializes a line of text into an entity or data model.
        /// </summary>
        /// <param name="line">The line of text to be parsed.</param>
        /// <returns>The data model represented by the text.</returns>
        T Deserialize(string line);
    }
}
