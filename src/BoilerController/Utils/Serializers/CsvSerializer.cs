using System.Text;

namespace BoilerController.Utils.Serializers
{
    internal abstract class CsvSerializer<T> : ISerializer<T>
        where T : class, new()
    {
        public abstract string Serialize(T entity);

        public abstract T Deserialize(string line);

        public string Escape(string data)
        {
            return $"\"{data.Replace("\"", "\"\"")}\"";
        }

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
            AddPart();

            return parts;
        }
    }
}
