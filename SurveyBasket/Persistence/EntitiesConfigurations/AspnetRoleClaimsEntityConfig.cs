using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Utilities.Encoders;
using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class AspnetRoleClaimsEntityConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var permissions = PermissionsClaims.GetPermissions();

        List<IdentityRoleClaim<string>> claimPermissions = [];

        for (int i = 0; i < permissions.Count; i++)
        {
            claimPermissions.Add(new IdentityRoleClaim<string>
            {
                Id = i + 1,
                ClaimType = PermissionsClaims.Type,
                ClaimValue = permissions[i],
                RoleId = DefaultRoles.AdminRoleId
            });
        }

        builder.HasData(claimPermissions);
    }
}
