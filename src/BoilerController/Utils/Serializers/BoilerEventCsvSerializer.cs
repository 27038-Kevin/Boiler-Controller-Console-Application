using BoilerController.Models;

namespace BoilerController.Utils.Serializers
{
    /// <summary>
    /// Serializes boiler events into a CSV file.
    /// </summary>
    internal class BoilerEventCsvSerializer : CsvSerializer<BoilerEvent>
    {
        /// <summary>
        /// Serializes an event log's info into a string that can be stored in a CSV file.
        /// </summary>
        /// <param name="eventInfo">The event information to be serialized.</param>
        /// <returns>A serialized string representing the event.</returns>
        public override string Serialize(BoilerEvent eventInfo)
        {
            return string.Join(
                    ',',
                    eventInfo.Timestamp,
                    Escape(eventInfo.Title),
                    Escape(eventInfo.Description));
        }

        /// <summary>
        /// Deserializes the string from the CSV file as a boiler event's information.
        /// </summary>
        /// <param name="line">The line to be deserialized.</param>
        /// <returns>The deserialized event data.</returns>
        /// <exception cref="FormatException">Thrown if the CSV format is incorrect, or the number of parameters is not expected.</exception>
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
