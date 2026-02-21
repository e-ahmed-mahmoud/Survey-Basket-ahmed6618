using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MimeKit;
using SurveyBasket.Extensions.Emails;

namespace SurveyBasket.Services.NotificaitonServices;

public class NotificaitonService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
 IHttpContextAccessor httpContextAccessor, IEmailSender emailSender) : INotificaitonService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task SendNewPollsNotificaiton(int? pollId = null)
    {
        IEnumerable<Poll> newPolls = [];

        if (pollId is not null)
        {
            var poll = await _dbContext.Polls.SingleOrDefaultAsync(p => p.Id == pollId && p.IsPublished);
            newPolls = [poll!];
        }
        else
        {
            newPolls = await _dbContext.Polls.Where(p => p.StartAt == DateOnly.FromDateTime(DateTime.UtcNow) && p.IsPublished).AsNoTracking().ToListAsync();
        }
        //ToDo Select Members users only depend on role
        var users = await _userManager.Users.Where(u => u.EmailConfirmed).Select(u => new { u.FirstName, u.Email, u.PhoneNumber }).ToListAsync();
        var orgin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        foreach (var poll in newPolls)
        {
            foreach (var user in users)
            {
                var placeholders = new Dictionary<string, string>()
                {
                    {"{{name}}",user.FirstName},
                    {"{{pollTill}}", poll.Title},
                    {"{{url}}",$"{orgin}/api/Polls/{pollId}/Votes/SubmitVote"},
                    {"{{endDate}}",poll.EndAt.ToString("yyyy-MM-dd")}

                };
                var emailMessage = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeholders);

                BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "New Poll Aded", emailMessage));
            }
        }
        await Task.CompletedTask;
    }
}