using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Surveys.Infrastructure.Contexts;

namespace Surveys.Infrastructure.DataSeeder
{
    public class DataSeeder : IDataSeeder
    {
        private readonly SurveysContext _context;
        private readonly UserManager<User> _userManager;

        public DataSeeder(SurveysContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void SeedDatabase()
        {
            SeedUserRoles();
            SeedUsers();
        }

        private void SeedUserRoles()
        {
            if (!_context.Roles.Any())
            {
                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = UserRoles.Administrator,
                        NormalizedName = UserRoles.Administrator.ToUpper()
                    },
                    new IdentityRole
                    {
                        Name = UserRoles.User,
                        NormalizedName = UserRoles.User.ToUpper()
                    }
                };

                _context.AddRange(roles);
                _context.SaveChanges();
            }
        }

        private void SeedUsers()
        {
            string password = "Password123!";
            string userEmail = "user@gmail.com";
            string administratorEmail = "administrator@gmail.com";

            if (!_context.Users.Any())
            {
                User administrator = new User
                {
                    Name = "Jan",
                    Surname = "Administrator",
                    UserName = administratorEmail,
                    NormalizedUserName = administratorEmail.ToUpper(),
                    Email = administratorEmail,
                    NormalizedEmail = administratorEmail.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                User user = new User
                {
                    Name = "Adam",
                    Surname = "User",
                    UserName = userEmail,
                    NormalizedUserName = userEmail.ToUpper(),
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                administrator.PasswordHash = _userManager.PasswordHasher
                    .HashPassword(administrator, password);
                user.PasswordHash = _userManager.PasswordHasher
                    .HashPassword(user, password);
                
                _context.Users.AddRange(administrator, user);
                _context.SaveChanges();
            }
        }
    }
}
