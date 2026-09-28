using BoilerController.Enums;

namespace BoilerController.Models
{
    internal class Boiler
    {
        internal static Boiler Instance { get; } = new();

        private Boiler()
        {
        }

        internal static bool IsReady => Instance.RunInterlockSwitch == RunInterlock.Closed;

        internal BoilerState SystemStatus { get; set; }

        internal RunInterlock RunInterlockSwitch { get; set; }
    }
}
