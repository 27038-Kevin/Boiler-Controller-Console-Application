using BoilerController.Models;
using BoilerController.Utils.Serializers;

namespace BoilerController.Repositories
{
    /// <summary>
    /// Abstracts create and read operations done on the boiler event log file.
    /// </summary>
    internal class BoilerEventRepository : IRepository<BoilerEvent>
    {
        private const string FileHeader = "Timestamp,Title,Description";

        private readonly string _filePath;
        private readonly ISerializer<BoilerEvent> _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoilerEventRepository"/> class.
        /// </summary>
        /// <param name="filePath">The path to the CSV file storing the event logs.</param>
        /// <param name="serializer">The serializer used for serializing/deserializing data to and from the file.</param>
        internal BoilerEventRepository(string filePath, ISerializer<BoilerEvent> serializer)
        {
            _filePath = filePath;
            _serializer = serializer;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, FileHeader + Environment.NewLine);
            }
        }

        /// <summary>
        /// Fetches all boiler event logs from the file.
        /// </summary>
        /// <returns>A lazy enumerable of the event logs.</returns>
        public IEnumerable<BoilerEvent> GetAll()
        {
            return File.ReadAllLines(_filePath)
                    .Select(_serializer.Deserialize);
        }

        /// <summary>
        /// Saves a new event log to the file.
        /// </summary>
        /// <param name="boilerEvent">The event to be saved.</param>
        public void Save(BoilerEvent boilerEvent)
        {
            File.AppendAllText(
                _filePath,
                _serializer.Serialize(boilerEvent) + Environment.NewLine);
        }
    }
}
