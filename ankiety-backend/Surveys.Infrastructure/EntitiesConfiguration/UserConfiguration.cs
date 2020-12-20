using Surveys.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Surveys.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Ignore(p => p.AccessFailedCount)
                .Ignore(p => p.LockoutEnabled)
                .Ignore(p => p.LockoutEnd)
                .Ignore(p => p.PhoneNumber)
                .Ignore(p => p.PhoneNumberConfirmed)
                .Ignore(p => p.TwoFactorEnabled);
        }
    }
}
