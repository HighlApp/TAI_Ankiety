using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Core.Entities;

namespace Surveys.Infrastructure.EntitiesConfiguration
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.Property(x => x.SendDate)
                .ValueGeneratedOnAdd();

            builder.HasIndex(x => x.SurveyId)
                .IsUnique(false);

            builder.HasIndex(x => x.UserId)
                .IsUnique(false);
        }
    }
}
