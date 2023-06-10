namespace HangfireExampleApp.Models
{
    public class ScheduledMessageModel
    {
        public string Message { get; set; }
        public string To { get; set; }
        public DateTime DateTime { get; set; }
    }
}
