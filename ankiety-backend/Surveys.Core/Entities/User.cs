using Microsoft.AspNetCore.Identity;

namespace Surveys.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
