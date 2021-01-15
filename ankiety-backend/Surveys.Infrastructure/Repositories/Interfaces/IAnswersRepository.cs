using System;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IAnswersRepository : IRepositoryBase<Answer>
    {
        Task<IEnumerable<Answer>> GetInvitationAnswers(Guid invitationId);
    }
}
