namespace SurveyBasket.Contracts.Dashboard;

public record PollVotesResponse(string Title, IEnumerable<VoteResponse> VoteResponses);

