using Microsoft.AspNetCore.OutputCaching;
using SurveyBasket.Services.VoteService;

namespace SurveyBasket.Controllers;

[ApiController]
[Route("api/Polls/{pollId}/[controller]")]
[Authorize]
public class VotesController(IQuestionService questionService, IVoteService voteService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;
    private readonly IVoteService _voteService = voteService;

    [HttpGet("[action]")]
    [OutputCache(PolicyName = "PollsOutputCache")]
    public async Task<IActionResult> GetPollQuestions([FromRoute] int pollId, CancellationToken cancellationToken)
    {
        var result = await _questionService.GetPollQuestionsAsync(pollId, User.GetUserId()!, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(result.Error.StatusCode);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SubmitVote([FromRoute] int pollId, [FromBody] VoteRequest request, CancellationToken cancellationToken)
    {
        var result = await _voteService.AddVoteASync(pollId, request, User.GetUserId()!, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem(result.Error.StatusCode);
    }

}
