namespace SurveyBasket.Contracts.Dashboard;

public record VoteResponse(string VoterName, DateTime VoteDate, IEnumerable<QuestionAnswerResponse> QuestionAnswerResponses);
