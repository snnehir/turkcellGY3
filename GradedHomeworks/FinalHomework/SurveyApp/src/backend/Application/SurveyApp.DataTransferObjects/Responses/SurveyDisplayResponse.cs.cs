using SurveyApp.DataTransferObjects.Requests;

namespace SurveyApp.DataTransferObjects.Responses
{
    public class SurveyDisplayResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SurveyStatus { get; set; }
        public string SurveyOwnerFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }

    }
    public static class SurveyStatus{
        public const string Pending = "Pending";
        public const string Active = "Active";
        public const string Closed = "Closed";
    }
}
