using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Contracts.Users;

public record RegisterRequest(string Email, string FirstName, string LastName, string PhoneNumber, string Password);
