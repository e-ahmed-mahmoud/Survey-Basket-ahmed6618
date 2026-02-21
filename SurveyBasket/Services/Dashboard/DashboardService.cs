
namespace SurveyBasket.Services.Dashboard;

public class DashboardService(ApplicationDbContext dbContext) : IDashboardService
{
    private readonly ApplicationDbContext _dbContext = dbContext;


    public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int pollId, CancellationToken cancellationToken)
    {
        var pollVotes = await _dbContext.Polls.Where(p => p.Id == pollId).AsNoTracking()
        .Select(p => new PollVotesResponse(p.Title,
            p.Votes.Select(v => new VoteResponse
            (
                $"{v.User.FirstName} {v.User.LastName}",
                VoteDate: v.SubmittedOn,
                QuestionAnswerResponses: v.VoteAnswers.Select(a => new QuestionAnswerResponse(a.Answer.Content, a.Question.Content)))
            ))).SingleOrDefaultAsync(cancellationToken);

        return pollVotes is null ? Result.Failure<PollVotesResponse>(PollErrors.NotDefinedError) : Result.Success(pollVotes);
    }

    public async Task<Result<IEnumerable<VotesPerDay>>> GetPollVotesPerDayAsync(int pollId, CancellationToken cancellationToken)
    {
        var pollIsExists = await _dbContext.Polls.AnyAsync(p => p.Id == pollId);
        if (!pollIsExists)
            return Result.Failure<IEnumerable<VotesPerDay>>(PollErrors.InvalidPollDataError);

        var pollVotesPerDay = await _dbContext.Votes.Where(v => v.PollId == pollId)
            .GroupBy(v => new { Date = DateOnly.FromDateTime(v.SubmittedOn) })
            .Select(g => new VotesPerDay(g.Key.Date, g.Count())).ToListAsync(cancellationToken);

        return pollVotesPerDay is null ? Result.Failure<IEnumerable<VotesPerDay>>(PollErrors.NotDefinedError) : Result.Success<IEnumerable<VotesPerDay>>(pollVotesPerDay);
    }

    public async Task<Result<IEnumerable<VotePerQuestionsResponse>>> GetPollVotesPerAnswerAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var pollIsExists = await _dbContext.Polls.AnyAsync(p => p.Id == pollId, cancellationToken);
        if (!pollIsExists)
            return Result.Failure<IEnumerable<VotePerQuestionsResponse>>(PollErrors.NotDefinedError);

        var pollQuestionAnswer = await _dbContext.VoteAnswers.Where(v => v.Vote.PollId == pollId)
            .Select(q => new VotePerQuestionsResponse(q.Question.Content,
                q.Question.VoteAnswers.GroupBy(x => new { AnswerId = x.Answer.Id, AnswerContent = x.Answer.Content })
                    .Select(g => new QuestionAnswerCount(g.Key.AnswerContent, g.Count()))))
            .ToListAsync(cancellationToken);

        return pollQuestionAnswer is not null || pollQuestionAnswer?.Count > 0 ?
            Result.Success<IEnumerable<VotePerQuestionsResponse>>(pollQuestionAnswer!) :
            Result.Failure<IEnumerable<VotePerQuestionsResponse>>(PollErrors.InvalidPollDataError);

    }

}
