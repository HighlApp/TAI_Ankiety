using Surveys.Core.Entities;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class AnswersRepository : RepositoryBase<Answer>, IAnswersRepository
    {
        public AnswersRepository(SurveysContext context) : base(context)
        {
        }
    }
}
