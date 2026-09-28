using BoilerController.Enums;
using BoilerController.Models;

namespace BoilerController.Services
{
    /// <summary>
    /// Service for boiler state management.
    /// Does not use a repository since <see cref="Boiler"/> is a singleton representing a state machine.
    /// </summary>
    internal static class BoilerService
    {
        /// <summary>
        /// Initializes the boiler's state to Lockout, and the run interlock switch to open.
        /// </summary>
        internal static void InitializeBoiler()
        {
            SetBoilerState(BoilerState.Lockout);
            Boiler.Instance.RunInterlockSwitch = RunInterlock.Open;
        }
        
        /// <summary>
        /// Resets the Lockout state if the boiler's run interlock switch is closed.
        /// Idempotent otherwise.
        /// </summary>
        /// <returns>Whether or not a state update took place.</returns>
        internal static bool ResetLockout()
        {
            if (Boiler.IsReady)
                SetBoilerState(BoilerState.Ready);

            return Boiler.IsReady;
        }

        /// <summary>
        /// Sets the boiler's internal system state.
        /// </summary>
        /// <param name="state">The state to change the boiler's state to.</param>
        internal static void SetBoilerState(BoilerState state)
        {
            Boiler.Instance.SystemStatus = state;
        }

        /// <summary>
        /// Simulates a boiler error when the boiler is ready and operational.
        /// </summary>
        /// <returns>Whether or not a simulated error was raised.</returns>
        internal static bool SimulateBoilerError()
        {
            if (Boiler.Instance.SystemStatus != BoilerState.Operational
                    && Boiler.Instance.SystemStatus != BoilerState.Ready)
                return false;

            SetBoilerState(BoilerState.Lockout);
            return true;
        }

        /// <summary>
        /// Toggles the run interlock switch.
        /// </summary>
        internal static void ToggleRunInterlockSwitch()
        {
            Boiler.Instance.RunInterlockSwitch = (Boiler.IsReady)
                                            ? RunInterlock.Open
                                            : RunInterlock.Closed;
        }
    }
}
