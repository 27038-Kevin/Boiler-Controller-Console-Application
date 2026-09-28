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

        public async Task Log(BoilerEvent boilerEvent)
        {
            await Task.Run(() => _repository.Save(boilerEvent));
        }
    }
}
