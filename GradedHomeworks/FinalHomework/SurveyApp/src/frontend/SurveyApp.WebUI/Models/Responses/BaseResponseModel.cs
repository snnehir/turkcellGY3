namespace SurveyApp.WebUI.Models.Responses
{
    public class BaseResponseModel<T>
    {
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string message { get; set; }
        public List<string> Errors { get; set; }
    }
}
