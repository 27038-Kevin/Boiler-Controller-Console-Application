using BoilerController.Enums;
using BoilerController.Models;
using BoilerController.Services;
using BoilerController.View;

namespace BoilerController.Controllers
{
    internal class ApplicationController : IDisposable
    {
        private bool _disposed;

        private readonly ConsoleOperations _ui;
        private readonly ILogger<BoilerEvent> _eventLogger;

        private event UIRenderer? RefreshView;

        internal ApplicationController(ConsoleOperations ui, ILogger<BoilerEvent> eventLogger)
        {
            _ui = ui;
            _eventLogger = eventLogger;

            RefreshView += _ui.ClearScreen;
            RefreshView += _ui.DisplayBoilerStatus(Boiler.Instance);
        }

        internal async Task Run()
        {
            await Initialize();
            
            UIRenderer menuDisplayer = _ui.DisplayMenu<MainMenu>();
            do
            {
                RefreshView += menuDisplayer;
                RefreshView.Invoke();
                RefreshView -= menuDisplayer;

                var choice = _ui.GetMenuOption<MainMenu>();
                switch (choice)
                {
                    case MainMenu.StartBoilerSequence:
                        await StartBoiler();
                        break;

                    case MainMenu.StopBoilerSequence:
                        await StopBoiler();
                        break;

                    case MainMenu.SimulateBoilerError:
                        await SimulateBoilerError();
                        break;

                    case MainMenu.ToggleRunInterlockSwitch:
                        await ToggleRunInterlock();
                        break;

                    case MainMenu.ResetLockout:
                        await ResetLockout();
                        break;

                    case MainMenu.ViewEventLog:
                        ViewEventLogs();    // No await; blocking operation anyway due to user input
                        break;

                    case MainMenu.ExitApplication:
                        Dispose();
                        return;
                }
            }
            while (true);
        }

        private async Task Ignition()
        {
            BoilerService.SetBoilerState(BoilerState.Ignition);
            await SimulateTimer("Ignition Stage", 10);

            UIRenderer refresher = _ui.DisplayMessage("Boiler Ignited Successfully.");
            RefreshView += refresher;
            RefreshView.Invoke();

            _eventLogger.Log(new()
            {
                Timestamp = DateTime.Now,
                Title = "Ignition",
                Description = "Ignition completed.",
            });

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private async Task Initialize()
        {
            BoilerService.InitializeBoiler();

            UIRenderer refresher = _ui.DisplayMessage("Boiler Controller Initialized.");
            RefreshView += refresher;
            RefreshView.Invoke();

            _eventLogger.Log(new()
            {
                Timestamp = DateTime.Now,
                Title = "Initialization",
                Description = "Boiler initialized.",
            });

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private async Task PrePurge()
        {
            BoilerService.SetBoilerState(BoilerState.PrePurge);
            await SimulateTimer("Pre-Purge Stage", 10);

            UIRenderer refresher = _ui.DisplayMessage("Boiler Pre-Purged Successfully.");
            RefreshView += refresher;
            RefreshView.Invoke();

            _eventLogger.Log(new()
            {
                Timestamp = DateTime.Now,
                Title = "Pre-Purge",
                Description = "Pre-Purge completed.",
            });

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private async Task ResetLockout()
        {
            string message = default!;
            BoilerEvent? boilerEvent = default;

            if (!BoilerService.ResetLockout())
            {
                message = "The Interlock Switch must be closed first.";
            }
            else
            {
                message = "Boiler Status Changed to Ready!";
                boilerEvent = new()
                {
                    Timestamp = DateTime.Now,
                    Title = "Lockout Reset",
                    Description = "Boiler status changed to Ready.",
                };
            }

            UIRenderer refresher = _ui.DisplayMessage(message);
            RefreshView += refresher;
            RefreshView.Invoke();

            if (boilerEvent is not null)
                _eventLogger.Log(boilerEvent);

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private async Task SimulateBoilerError()
        {
            string message = default!;
            BoilerEvent? boilerEvent = default;

            if (!BoilerService.SimulateBoilerError())
            {
                message = "Cannot simulate boiler error when boiler is not operational or ready!";
            }
            else
            {
                message =
@"Error: Boiler failed to start up for some unknown reason.
System in lockout.";

                boilerEvent = new()
                {
                    Timestamp = DateTime.Now,
                    Title = "Startup Failure",
                    Description = "The boiler failed to start up due to a simulated error.",
                };
            }
            
            UIRenderer refresher = _ui.DisplayMessage(message);
            RefreshView += refresher;
            RefreshView.Invoke();

            if (boilerEvent is not null)
                _eventLogger.Log(boilerEvent);

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private async Task SimulateTimer(string displayMessage, int seconds)
        {
            UIRenderer refresher;

            while (seconds > 0)
            {
                refresher = _ui.DisplayMessage(
$@"{displayMessage}
Time remaining: {seconds} seconds");
                RefreshView += refresher;
                RefreshView.Invoke();
                RefreshView -= refresher;
                
                await Task.Delay(1000);
                --seconds;
            }

        }

        private async Task StartBoiler()
        {
            if (!Boiler.IsReady)
            {
                UIRenderer refresher = _ui.DisplayMessage("The Interlock Switch must be Closed first.");
                RefreshView += refresher;
                RefreshView.Invoke();
                RefreshView -= refresher;
                await Task.Delay(2500);
                return;
            }

            await PrePurge();
            await Ignition();

            BoilerService.SetBoilerState(BoilerState.Operational);
            _eventLogger.Log(new()
            {
                Timestamp = DateTime.Now,
                Title = "Operational",
                Description = "Boiler is now operational.",
            });
        }

        private async Task StopBoiler()
        {
            BoilerService.InitializeBoiler();   // Re-initialize boiler to restore to default state

            UIRenderer refresher = _ui.DisplayMessage("Boiler Reset Successfully.");
            RefreshView += refresher;
            RefreshView.Invoke();

            _eventLogger.Log(new()
            {
                Timestamp = DateTime.Now,
                Title = "Reset",
                Description = "Boiler was stopped and reset to default state.",
            });

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private async Task ToggleRunInterlock()
        {
            BoilerService.ToggleRunInterlockSwitch();

            UIRenderer refresher = _ui.DisplayMessage($"Switch is now toggled to: {Boiler.Instance.RunInterlockSwitch}");
            RefreshView += refresher;
            RefreshView.Invoke();

            _eventLogger.Log(new()
            {
                Timestamp = DateTime.Now,
                Title = "Interlock",
                Description = $"Interlock switch toggled to {Boiler.Instance.RunInterlockSwitch}",
            });

            await Task.Delay(2500);
            RefreshView -= refresher;
        }

        private void ViewEventLogs()
        {
            UIRenderer logDisplayer = _ui.DisplayEventLogs(_eventLogger.ReadLogs());
            RefreshView += logDisplayer;
            RefreshView.Invoke();
            RefreshView -= logDisplayer;
        }

        public void Dispose()
        {
            if (!_disposed)
                RefreshView = null;

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
