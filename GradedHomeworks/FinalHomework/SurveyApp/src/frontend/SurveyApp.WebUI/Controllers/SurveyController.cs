using Microsoft.AspNetCore.Mvc;
using SurveyApp.WebUI.Models.Requests;
using SurveyApp.WebUI.Models.Responses;
using SurveyApp.WebUI.Services.Survey;

namespace SurveyApp.WebUI.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        
        public async Task<IActionResult> Index(string surveyId)
        {
            var surveyGuid = Guid.Parse(surveyId);
            var survey = await _surveyService.GetSurveyById(surveyGuid);
            return View(survey);
        }

        [HttpPost]
        public async Task<IActionResult> SendSurvey(SendSurveyResponseRequestModel request)
        {
            await _surveyService.SendSurveyResponse(request);
            return View();
        }

    }
}
