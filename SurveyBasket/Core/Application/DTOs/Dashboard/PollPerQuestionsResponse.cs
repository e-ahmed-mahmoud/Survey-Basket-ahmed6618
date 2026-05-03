namespace SurveyBasket.Contracts.Dashboard;

public record VotePerQuestionsResponse(string Content, IEnumerable<QuestionAnswerCount> SelectedAnswerCount);
