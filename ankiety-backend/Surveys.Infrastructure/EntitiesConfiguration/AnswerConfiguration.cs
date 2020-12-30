using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Core.Entities;

namespace Surveys.Infrastructure.EntitiesConfiguration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(x => x.QuestionId)
                .IsUnique(false);
        }
    }
}
