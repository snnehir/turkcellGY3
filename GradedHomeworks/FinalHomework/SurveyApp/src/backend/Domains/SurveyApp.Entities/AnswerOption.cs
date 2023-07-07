namespace SurveyApp.Entities
{
    public class AnswerOption: IEntity
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public int QuestionOptionId { get; set; }
        public QuestionOption QuestionOption { get; set; }
    }
}
