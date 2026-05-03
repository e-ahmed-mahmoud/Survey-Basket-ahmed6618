namespace SurveyBasket.Core.Application.Interfaces;

public interface IVoteService
{
    Task<Result> AddVoteASync(int pollId, VoteRequest request, string userId, CancellationToken cancellationToken = default);
}
