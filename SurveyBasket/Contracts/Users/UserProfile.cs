using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Contracts.Users;

public record UserProfile(string FullName, string Email, string PhoneNumber, string UserName);
