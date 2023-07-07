using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services.Services.ResponseService
{
    public interface IResponseService
    {

        Task CreateSurveyResponse(SurveyResponseModel model);
        Task<ICollection<Response>> GetResponsesOfSurvey(Guid surveyId);
    }
}
