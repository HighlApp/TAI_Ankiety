using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Core.Entities;

namespace Surveys.Infrastructure.EntitiesConfiguration
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.Property(x => x.OptionText)
                .IsRequired();

            builder.HasOne(x => x.Question)
                .WithMany(x => x.Options);
        }
    }
}
