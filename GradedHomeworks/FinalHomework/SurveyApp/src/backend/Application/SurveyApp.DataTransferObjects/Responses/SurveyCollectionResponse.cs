namespace SurveyApp.DataTransferObjects.Responses
{
    public class SurveyCollectionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SurveyStatus { get; set; }
        public DateTime? EndDate { get; set; }
        public int ResponseCount { get; set; }
    }
}
