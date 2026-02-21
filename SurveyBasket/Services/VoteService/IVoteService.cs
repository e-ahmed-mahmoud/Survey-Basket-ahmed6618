using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyBasket.Contracts.Votes;

namespace SurveyBasket.Services.VoteService;

public interface IVoteService
{
    Task<Result> AddVoteASync(int pollId, VoteRequest request, string userId, CancellationToken cancellationToken = default);
}
