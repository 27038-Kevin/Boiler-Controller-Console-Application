namespace BoilerController.Models
{
    internal class EventInfo
    {
        public DateTime Timestamp { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
