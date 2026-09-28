using System.Threading.Tasks;
using BoilerController.Models;
using BoilerController.Utils.Serializers;

namespace BoilerController.Repositories
{
    internal class BoilerEventRepository : IRepository<BoilerEvent>
    {
        private const string FileHeader = "Timestamp,Title,Description";

        private readonly string _filePath;
        private readonly ISerializer<BoilerEvent> _serializer;

        internal BoilerEventRepository(string filePath, ISerializer<BoilerEvent> serializer)
        {
            _filePath = filePath;
            _serializer = serializer;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, FileHeader + Environment.NewLine);
            }
        }

        public IEnumerable<BoilerEvent> GetAll()
        {
            return File.ReadAllLines(_filePath)
                    .Select(_serializer.Deserialize);
        }

        public void Save(BoilerEvent boilerEvent)
        {
            File.AppendAllText(
                _filePath,
                _serializer.Serialize(boilerEvent) + Environment.NewLine);
        }
    }
}
