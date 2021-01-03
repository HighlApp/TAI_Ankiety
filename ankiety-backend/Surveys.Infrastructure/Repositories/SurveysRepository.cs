using Surveys.Core.Entities;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class SurveysRepository : RepositoryBase<Survey>, ISurveysRepository
    {
        public SurveysRepository(SurveysContext context) : base(context)
        {
        }
    }
}
