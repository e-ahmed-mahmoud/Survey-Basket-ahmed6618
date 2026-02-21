using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class AnswerEntityConfig : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasIndex(a => new { a.Id, a.QuestionId }).IsUnique();

        builder.Property(a => a.Content).HasMaxLength(1000);

        
    }
}
