using BoilerController.Models;

namespace BoilerController.Utils.Serializers
{
    internal class BoilerEventCsvSerializer : CsvSerializer<BoilerEvent>
    {
        public override string Serialize(BoilerEvent eventInfo)
        {
            return string.Join(',', eventInfo.Timestamp, eventInfo.Title, eventInfo.Description);
        }

        public override BoilerEvent Deserialize(string line)
        {
            List<string> parts = ParseCsv(line);
            if (parts.Count != 3)
            {
                throw new FormatException("Invalid CSV format");
            }

            DateTime timestamp = DateTime.Parse(parts[0]);
            string title = parts[1];
            string description = parts[2];

            return new BoilerEvent
            {
                Timestamp = timestamp,
                Title = title,
                Description = description,
            };
        }
    }
}
