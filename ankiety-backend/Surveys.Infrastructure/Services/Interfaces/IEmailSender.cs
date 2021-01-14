using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendInvitationEmailAsync(string emailAddress, DateTime? startDate, DateTime? expirationDate);
    }
}
