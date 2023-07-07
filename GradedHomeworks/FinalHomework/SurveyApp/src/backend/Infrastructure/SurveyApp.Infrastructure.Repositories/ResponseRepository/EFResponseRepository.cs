using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories.ResponseRepository
{
    public class EFResponseRepository : IResponseRepository
    {
        private readonly SurveyAppDbContext _dbContext;

        public EFResponseRepository(SurveyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Response entity)
        {
            await _dbContext.Responses.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Response>> GetAllAsync()
        {
            return await _dbContext.Responses.ToListAsync();
        }

        public async Task UpdateAsync(Response entity)
        {
            _dbContext.Responses.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Response>> GetResponseOfSurvey(Guid surveyId)
        {
            return await _dbContext.Responses.Include(r => r.Answers).ThenInclude(ra => ra.AnswerOptions)
                                      .Where(r => r.SurveyId.Equals(surveyId)).ToListAsync();
        }

    }
}
