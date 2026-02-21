
namespace SurveyBasket.Services.Dashboard;

public interface IDashboardService
{
    Task<Result<PollVotesResponse>> GetPollVotesAsync(int pollId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<VotesPerDay>>> GetPollVotesPerDayAsync(int pollId, CancellationToken cancellationToken);

    Task<Result<IEnumerable<VotePerQuestionsResponse>>> GetPollVotesPerAnswerAsync(int pollId, CancellationToken cancellationToken = default);
}
