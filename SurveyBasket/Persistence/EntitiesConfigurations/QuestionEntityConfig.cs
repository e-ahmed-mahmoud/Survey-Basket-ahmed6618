using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class QuestionEntityConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasIndex(q => new { q.PollId, q.Content }).IsUnique();

        builder.Property(q => q.Content).HasMaxLength(1000);
    }
}
