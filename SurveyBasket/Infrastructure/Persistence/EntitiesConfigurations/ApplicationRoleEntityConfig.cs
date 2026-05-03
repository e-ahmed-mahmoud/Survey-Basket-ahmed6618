using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class ApplicationRoleEntityConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData([
            new ApplicationRole{
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                ConcurrencyStamp = DefaultRoles.AdminConcurrencyStamp,
                NormalizedName = DefaultRoles.Admin.ToUpper()
            },
            new ApplicationRole{
                Id = DefaultRoles.MemberRoleId,
                Name = DefaultRoles.Member,
                ConcurrencyStamp = DefaultRoles.MemberConcurrencyStamp,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                IsDefault = true
            }
        ]);
    }
}
