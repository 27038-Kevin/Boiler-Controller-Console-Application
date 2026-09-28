using BoilerController.Enums;
using BoilerController.Models;

namespace BoilerController.Services
{
    internal static class BoilerService
    {
        internal static void InitializeBoiler()
        {
            SetBoilerState(BoilerState.Lockout);
            Boiler.Instance.RunInterlockSwitch = RunInterlock.Open;
        }
        
        internal static bool ResetLockout()
        {
            if (Boiler.IsReady)
                SetBoilerState(BoilerState.Ready);

            return Boiler.IsReady;
        }

        internal static void SetBoilerState(BoilerState state)
        {
            Boiler.Instance.SystemStatus = state;
        }

        internal static bool SimulateBoilerError()
        {
            if (Boiler.Instance.SystemStatus != BoilerState.Operational
                    && Boiler.Instance.SystemStatus != BoilerState.Ready)
                return false;

            SetBoilerState(BoilerState.Lockout);
            return true;
        }

        internal static void ToggleRunInterlockSwitch()
        {
            Boiler.Instance.RunInterlockSwitch = (Boiler.IsReady)
                                            ? RunInterlock.Open
                                            : RunInterlock.Closed;
        }
    }
}
