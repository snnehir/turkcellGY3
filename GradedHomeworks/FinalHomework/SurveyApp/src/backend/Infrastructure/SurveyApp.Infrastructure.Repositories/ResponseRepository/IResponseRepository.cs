﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories.ResponseRepository
{
    public interface IResponseRepository : IRepository<Response>
    {
        Task<IList<Response>> GetResponseOfSurvey(Guid surveyId);
    }
}
