using SurveyApp.Infrastructure.Repositories.ResponseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services.Services.ResponseService
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;

        public ResponseService(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }

        public async Task CreateSurveyResponse(SurveyResponseModel model)
        {
            var response = model.Adapt<Response>();
            foreach (var responseItem in response.Answers) { 
                responseItem.Id = 0;
                foreach(var answerOption in responseItem.AnswerOptions)
                {
                    answerOption.Id = 0;
                }
            }  
            response.SurveyId = Guid.Parse(model.SurveyId);
            await _responseRepository.CreateAsync(response);
        }

        public async Task<ICollection<Response>> GetResponsesOfSurvey(Guid surveyId)
        {
           
            return await _responseRepository.GetResponseOfSurvey(surveyId);
        }
    }
}
