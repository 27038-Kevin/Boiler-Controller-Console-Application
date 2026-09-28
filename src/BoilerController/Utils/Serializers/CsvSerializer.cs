using System.Text;

namespace BoilerController.Utils.Serializers
{
    /// <summary>
    /// Serializes data to and from CSV (comma-separated value) format.
    /// </summary>
    /// <typeparam name="T">The generic type parameter to be serialized/deserialized.</typeparam>
    internal abstract class CsvSerializer<T> : ISerializer<T>
        where T : class, new()
    {
        /// <inheritdoc cref="ISerializer{T}.Serialize(T)"/>
        public abstract string Serialize(T entity);

        /// <inheritdoc cref="ISerializer{T}.Deserialize"/>
        public abstract T Deserialize(string line);

        /// <summary>
        /// Escapes special characters that are used as delimiters in a CSV file for storage.
        /// </summary>
        /// <param name="data">The data to be encoded and stored.</param>
        /// <returns>An escaped string.</returns>
        public string Escape(string data)
        {
            return $"\"{data.Replace("\"", "\"\"")}\"";
        }

        /// <summary>
        /// Parses the CSV line and returns a list of parsed values.
        /// </summary>
        /// <param name="line">The line to be parsed.</param>
        /// <returns>The list of parameters encoded in the CSV line.</returns>
        public List<string> ParseCsv(string line)
        {
            var parts = new List<string>();
            var part = new StringBuilder();
            bool inQuotes = false;

            void AddPart()
            {
                parts.Add(part.ToString());
                part.Clear();
                inQuotes = false;
            }

            for (int i = 0; i < line.Length; ++i)
            {
                switch (line[i])
                {
                    case '"':
                        if (!inQuotes)
                        {
                            inQuotes = true;
                        }
                        else if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            part.Append(line[i]);
                            ++i;
                        }
                        else
                        {
                            AddPart();
                            ++i;
                        }
                        break;

                    case ',':
                        if (inQuotes)
                        {
                            part.Append(line[i]);
                        }
                        else
                        {
                            AddPart();
                        }
                        break;

                    case '\r':
                        break;

                    case '\n':
                        if (inQuotes)
                        {
                            part.Append(line[i]);
                        }
                        else
                        {
                            AddPart();
                        }
                        break;

                    default:
                        part.Append(line[i]);
                        break;
                }
            }

            return parts;
        }
    }
}
