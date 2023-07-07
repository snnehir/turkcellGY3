using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Services.Services.ResponseService;
using SurveyApp.Services.Services.SurveyService;

namespace SurveyApp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IResponseService _responseService;
        public SurveyController(ISurveyService surveyService, IResponseService responseService)
        {
            _surveyService = surveyService;
            _responseService = responseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyRequest request)
        {
            var response = await _surveyService.CreateSurvey(request);
            return Ok(response);

        }


        [HttpGet("all")]
        public async Task<IActionResult> GetSurveys()
        {
            var response = await _surveyService.GetUserSurveys();
            return Ok(response);

        }

        [HttpGet("[action]/{surveyId}")]
        public async Task<IActionResult> Responses(Guid surveyId)
        {
            var response = await _responseService.GetResponsesOfSurvey(surveyId);
            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost("{surveyId}")]
        public async Task<IActionResult> GetSurveyById(Guid surveyId)
        {
            var response = await _surveyService.GetSurveyById(surveyId);
            if(response == null)
            {
                var responseFail = BaseResponseModel<SurveyDisplayResponse>.Fail(ConstantMessages.SurveyNotFound);
                return NotFound(responseFail);
            }
            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> SendResponse(SurveyResponseModel model)
        {
            await _responseService.CreateSurveyResponse(model);
            return Ok();
        }


    }
}
