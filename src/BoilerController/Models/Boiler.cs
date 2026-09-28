using BoilerController.Enums;

namespace BoilerController.Models
{
    /// <summary>
    /// State machine representing the boiler during runtime.
    /// Implemented as a singleton since each controller instance has a one-to-one mapping to a single boiler.
    /// </summary>
    internal class Boiler
    {
        /// <summary>
        /// The static <see cref="Boiler"/> instance controlled by the application.
        /// </summary>
        internal static Boiler Instance { get; } = new();

        /// <summary>
        /// Default constructor declared as private to prevent external initialization.
        /// </summary>
        private Boiler()
        {
        }

        /// <summary>
        /// Gets whether the boiler is ready and operational; i.e., whether the run interlock switch is closed.
        /// </summary>
        internal static bool IsReady => Instance.RunInterlockSwitch == RunInterlock.Closed;

        /// <summary>
        /// Gets or sets the current status of the boiler.
        /// </summary>
        internal BoilerState SystemStatus { get; set; }

        /// <summary>
        /// Gets or sets the currents state of the run interlock switch.
        /// </summary>
        internal RunInterlock RunInterlockSwitch { get; set; }
    }
}
