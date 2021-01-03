using Surveys.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IQuestionsRepository : IRepositoryBase<Question>
    {
        Task<Question> GetByIdWithOptions(Guid id);
    }
}
