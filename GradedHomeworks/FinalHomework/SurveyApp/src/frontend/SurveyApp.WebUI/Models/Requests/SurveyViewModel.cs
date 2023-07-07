using SurveyApp.WebUI.Models.Responses;

namespace SurveyApp.WebUI.Models.Requests
{
    public class SurveyViewModel
    {
        public SendSurveyResponseRequestModel Response { get; set; }
        public SurveyResponseModel Survey { get; set; }
    }
}
