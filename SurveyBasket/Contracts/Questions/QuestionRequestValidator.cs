namespace SurveyBasket.Contracts.Questions;

public class QuestionsRequestValidator : AbstractValidator<QuestionRequest>
{
    public QuestionsRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Question content is required.")
            .Length(3, 1000).WithMessage("Question content must be between 3 and 1000 characters.");

        RuleFor(x => x.Answers).Must(x => x.Count > 2).WithMessage("At least 3 answers are required.");

        RuleFor(x => x.Answers).Must(x => x.Distinct().Count() == x.Count).WithMessage("Answers must be unique.");

    }
}
