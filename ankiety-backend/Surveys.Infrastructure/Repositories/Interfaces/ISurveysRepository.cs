using System;
using Surveys.Core.Entities;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface ISurveysRepository : IRepositoryBase<Survey>
    {
        Task<Survey> GetByIdWithQuestionsAndAnswerOptionsAsync(Guid id);
    }
}
