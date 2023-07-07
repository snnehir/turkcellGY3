using SurveyApp.Entities;

namespace SurveyApp.DataTransferObjects.Requests
{
    public class CreateSurveyRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SurveyStatus { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}
