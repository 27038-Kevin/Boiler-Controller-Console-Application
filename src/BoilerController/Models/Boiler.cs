using BoilerController.Enums;

namespace BoilerController.Models
{
    internal class Boiler
    {
        internal static Boiler? Instance { get; }

        private Boiler()
        {
        }

        internal BoilerState SystemStatus { get; set; }

        internal RunInterlock RunInterlockSwitch { get; set; }
    }
}
