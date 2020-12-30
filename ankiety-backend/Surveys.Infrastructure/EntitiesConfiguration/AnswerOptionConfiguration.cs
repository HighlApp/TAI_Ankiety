using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Core.Entities;

namespace Surveys.Infrastructure.EntitiesConfiguration
{
    public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
    {
        public void Configure(EntityTypeBuilder<AnswerOption> builder)
        {
            builder.Property(x => x.Text)
                .IsRequired();

            builder.HasOne(x => x.Question)
                .WithMany(x => x.AnswerOptions);
        }
    }
}
