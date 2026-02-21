using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class AspnetUserEntityRolesConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData([
           new IdentityUserRole<string> {
               UserId = DefaultUsers.AdminId,
               RoleId = DefaultRoles.AdminRoleId
           }
        ]);
    }
}
