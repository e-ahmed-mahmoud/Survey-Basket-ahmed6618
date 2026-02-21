using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Abstractions;

public static class DefaultUsers
{
    public const string AdminId = "48606D7C-F8DA-406D-B9B6-C1107E14A515";
    public const string AdminEmail = "admin@SarveyBsket.com";
    public const string SecurityStamp = "76F99E5872284B728AAA17AA39E1E66E";
    public const string ConcurencyStamp = "FC9634F6-FD78-4473-969B-AB0FA9465917";
    public const string AdminPassword = "Passw@rd1234";
    public const string AdminPasswordHached = "AQAAAAIAAYagAAAAENqaBNM8gVLSYfEdnQc8XYb/I2YIIS8gD1RVqyausCUQ0xt0LAzommCCVvD+dPBfPw==";


}

// public static class GeneratePasswordHasher
// {
//     public static string Generate(string password, ApplicationUser applicationUser = null!)
//     {
//         var passwordHasher = new PasswordHasher<ApplicationUser>();
//         return passwordHasher.HashPassword(applicationUser, password);
//     }
// }

