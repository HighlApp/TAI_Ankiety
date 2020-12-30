using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Core.Entities;

namespace Surveys.Infrastructure.EntitiesConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.Text)
                .IsRequired();

            builder.Property(x => x.Created)
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.QuestionType)
                .IsRequired();

            builder.HasOne(x => x.Survey)
                .WithMany(x => x.Questions);
        }
    }
}
