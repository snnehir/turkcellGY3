using Microsoft.Extensions.Configuration;
using SurveyApp.Infrastructure.Repositories.SurveyRepository;

namespace SurveyApp.Services.Services.SurveyService
{
    public class SurveyService: ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public SurveyService(ISurveyRepository surveyRepository, IUserService userService, IConfiguration configuration)
        {
            _surveyRepository = surveyRepository;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<CreateSurveyResponse> CreateSurvey(CreateSurveyRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            // check if user exist
            var userExist = await _userService.CheckUserExistById(userId);
            if (!userExist)
            {
                // exception?
            }
            var survey = request.Adapt<Survey>();
            survey.SurveyOwnerId = userId;
            await _surveyRepository.CreateAsync(survey);
            var webUrl = _configuration["WebUrl"];
            var surveyPath = $"{webUrl}Survey?surveyId={survey.Id}";
            var response = new CreateSurveyResponse()
            {
                SurveyPath = surveyPath,
                SurveyTitle = survey.Title,
            };
            return response;
            
        }

        public async Task<IEnumerable<SurveyCollectionResponse>> GetUserSurveys()
        {
            var userId = await _userService.GetCurrentUserId();
            // check if user exist
            var userExist = await _userService.CheckUserExistById(userId);
            if (!userExist)
            {
                // exception?
            }
            var surveys = await _surveyRepository.GetUserSurveys(userId);
            var surveyDto = surveys.Adapt<IEnumerable<SurveyCollectionResponse>>();
            return surveyDto;

        }

        public async Task<SurveyDisplayResponse> GetSurveyById(Guid surveyId)
        {
            var survey = await _surveyRepository.GetByIdAsync(surveyId);
            if(survey == null || !survey.SurveyStatus.Equals(SurveyStatus.Active))
            {
                return null;
            }

            var surveyDto = survey.Adapt<SurveyDisplayResponse>();
            return surveyDto;
        }
    }
}
