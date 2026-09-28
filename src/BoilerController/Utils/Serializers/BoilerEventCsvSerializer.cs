using BoilerController.Models;

namespace BoilerController.Utils.Serializers
{
    internal class BoilerEventCsvSerializer : CsvSerializer<EventInfo>
    {
        public override string Serialize(EventInfo eventInfo)
        {
            return string.Join(',', eventInfo.Timestamp, eventInfo.Title, eventInfo.Description);
        }

        public override EventInfo Deserialize(string line)
        {
            List<string> parts = ParseCsv(line);
            if (parts.Count != 3)
            {
                throw new FormatException("Invalid CSV format");
            }

            DateTime timestamp = DateTime.Parse(parts[0]);
            string title = parts[1];
            string description = parts[2];

            return new EventInfo
            {
                Timestamp = timestamp,
                Title = title,
                Description = description,
            };
        }
    }
}
