namespace SurveyApp.WebUI.Models.Responses
{
    public class SurveyResponseModel
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

    public enum QuestionType
    {
        MultipleChoice = 1,
        SingleChoice = 2,
        TextInput = 3,
        TextArea = 4,
        Rating = 5
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsMandatory { get; set; }
        public int QuestionTypeId { get; set; }
        public ICollection<QuestionOptionDto>? QuestionOptions { get; set; }
    }

    public class QuestionOptionDto
    {
        public string Option { get; set; }
        public int Id { get; set; }
    }
}
