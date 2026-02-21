using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Abstractions.Const;

public static class DefaultRoles
{
    public const string Admin = nameof(Admin);
    public const string AdminRoleId = "19FAAB86-DF30-4853-9BC9-243B30887F65";
    public const string AdminConcurrencyStamp = "9B350BF4-A697-4FF0-BD33-79D3C32E3C50";
    public const string Member = nameof(Member);
    public const string MemberRoleId = "8B51DB17-0E93-48E0-A701-FB962BDBAA26";
    public const string MemberConcurrencyStamp = "9B350BF4-A697-4FF0-BD33-79D3C32E3C50";
}
