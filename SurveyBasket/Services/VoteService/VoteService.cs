using SurveyBasket.Contracts.Votes;

namespace SurveyBasket.Services.VoteService;

public class VoteService(ApplicationDbContext dbContext) : IVoteService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result> AddVoteASync(int pollId, VoteRequest request, string userId, CancellationToken cancellationToken = default)
    {
        var pollExists = await _dbContext.Polls.AnyAsync(p => p.Id == pollId, cancellationToken);
        if (!pollExists)
            return Result.Failure(new Error("Poll not found", "The specified poll does not exist.", 404));

        var userHasVoted = await _dbContext.Votes.AnyAsync(v => v.PollId == pollId && v.UserId == userId, cancellationToken);
        if (userHasVoted)
            return Result.Failure(new Error("Duplicate vote", "The user has already voted in this poll.", 409));

        var pollQuestions = await _dbContext.Questions.Where(q => q.PollId == pollId && q.IsActive).AsNoTracking().Select(q => q.Id).ToListAsync(cancellationToken);

        var VoteQuestionsIds = request.VoteAnswers.Select(a => a.QuestionId).ToList();
        if (!pollQuestions.SequenceEqual(VoteQuestionsIds))
            return Result.Failure(new Error("Invalid vote", "The vote answers do not match the poll questions.", 400));

        var vote = new Vote
        {
            PollId = pollId,
            UserId = userId,
            SubmittedOn = DateTime.UtcNow,
            VoteAnswers = [.. request.VoteAnswers.Adapt<IEnumerable<VoteAnswer>>()]
        };

        await _dbContext.Votes.AddAsync(vote, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
