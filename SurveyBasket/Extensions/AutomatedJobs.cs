using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Hangfire;
using SurveyBasket.Services.NotificaitonServices;

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
