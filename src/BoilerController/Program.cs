using BoilerController.Controllers;
using BoilerController.Models;
using BoilerController.Repositories;
using BoilerController.Services;
using BoilerController.Utils.Serializers;
using BoilerController.View;

namespace BoilerController
{
    internal class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.CsvFileName);

            var consoleOps = new ConsoleOperations();
            ISerializer<BoilerEvent> serializer = new BoilerEventCsvSerializer();
            IRepository<BoilerEvent> repository = new BoilerEventRepository(filePath, serializer);
            ILogger<BoilerEvent> logger = new EventLoggerService(repository);

            using var app = new ApplicationController(consoleOps, logger);
            try
            {
                await app.Run();
            }
            catch (Exception ex)
            {
                logger.Log(new()
                {
                    Timestamp = DateTime.Now,
                    Title = "Application Crash",
                    Description =
@$"Could not start up boiler controller.
Exception Message: {ex.Message}
Exception Stack Trace: {ex.StackTrace}",
                });
            }
        }
    }
}
