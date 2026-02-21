using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Services.NotificaitonServices;

public interface INotificaitonService
{
    Task SendNewPollsNotificaiton(int? pollId = null);
}
