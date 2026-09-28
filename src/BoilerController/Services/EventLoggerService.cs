using BoilerController.Models;
using BoilerController.Repositories;

namespace BoilerController.Services
{
    /// <summary>
    /// Service that manages event logging and handling file operations.
    /// </summary>
    internal class EventLoggerService : ILogger<BoilerEvent>
    {
        private readonly IRepository<BoilerEvent> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLoggerService"/> class.
        /// </summary>
        /// <param name="repository">The repository used for abstracting file operations.</param>
        internal EventLoggerService(IRepository<BoilerEvent> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Logs a new event to the file.
        /// </summary>
        /// <param name="boilerEvent">The event to be logged.</param>
        public void Log(BoilerEvent boilerEvent)
        {
            _repository.Save(boilerEvent);
        }

        /// <summary>
        /// Reads the event logs and returns all event logs from the file.
        /// </summary>
        /// <returns>A lazy enumerable of all event logs.</returns>
        public IEnumerable<BoilerEvent> ReadLogs()
        {
            return _repository.GetAll();
        }
    }
}
