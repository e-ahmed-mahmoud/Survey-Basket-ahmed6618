namespace SurveyBasket.Core.Application.Interfaces;

public interface INotificaitonService
{
    Task SendNewPollsNotificaiton(int? pollId = null);
}
