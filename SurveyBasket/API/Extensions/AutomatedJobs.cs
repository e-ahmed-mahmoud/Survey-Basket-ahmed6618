using Hangfire;
using SurveyBasket.Core.Application.Interfaces;

namespace SurveyBasket.Extensions;

public static class AutomatedJobs
{
    public static IApplicationBuilder InvokeAutomatedHangfireJobs(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var scope = scopeFactory.CreateScope();

        var notificaitonService = scope.ServiceProvider.GetRequiredService<INotificaitonService>();

        RecurringJob.AddOrUpdate("SendNewPollsNotificaiton", () => notificaitonService.SendNewPollsNotificaiton(null), Cron.Daily);

        return app;
    }
}
