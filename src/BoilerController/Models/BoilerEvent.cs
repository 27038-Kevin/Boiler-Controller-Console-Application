namespace BoilerController.Models
{
    /// <summary>
    /// Represents an event handled and/or logged by the boiler controller application.
    /// </summary>
    internal class BoilerEvent
    {
        /// <summary>
        /// Gets or sets the timestamp at which the event occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the event's title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
