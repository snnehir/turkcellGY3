using SurveyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories.SurveyRepository
{
    public class EFSurveyRepository : ISurveyRepository
    {
        private readonly SurveyAppDbContext _dbContext;

        public EFSurveyRepository(SurveyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Survey entity)
        {
            entity.Id = Guid.NewGuid();
            await _dbContext.Surveys.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Survey>> GetAllAsync()
        {
            return await _dbContext.Surveys.ToListAsync();
        }

        
        public async Task UpdateAsync(Survey entity)
        {
            _dbContext.Surveys.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IList<Survey>> GetUserSurveys(int userId)
        {
            return await _dbContext.Surveys.Where(s => s.SurveyOwnerId  == userId).Include(s => s.Responses).ToListAsync();
        }

        public async Task<Survey?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Surveys.Include(s=>s.SurveyOwner)
                                           .Include(s => s.Questions).ThenInclude(q => q.QuestionOptions)
                                           .SingleOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Remove(id);
        }
      
    }
}
