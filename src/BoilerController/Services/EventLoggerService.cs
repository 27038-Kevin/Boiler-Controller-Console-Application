using BoilerController.Models;
using BoilerController.Repositories;

namespace BoilerController.Services
{
    internal class EventLoggerService : ILogger<BoilerEvent>
    {
        private readonly IRepository<BoilerEvent> _repository;

        internal EventLoggerService(IRepository<BoilerEvent> repository)
        {
            _repository = repository;
        }

        public void Log(BoilerEvent boilerEvent)
        {
            _repository.Save(boilerEvent);
        }

        public IEnumerable<BoilerEvent> ReadLogs()
        {
            return _repository.GetAll();
        }
    }
}
