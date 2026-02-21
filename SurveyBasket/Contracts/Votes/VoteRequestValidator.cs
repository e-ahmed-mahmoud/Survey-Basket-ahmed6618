using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Contracts.Votes;

public class VoteRequestValidator : AbstractValidator<VoteRequest>
{
    public VoteRequestValidator()
    {
        RuleFor(v => v.VoteAnswers).NotEmpty();
        RuleForEach(x => x.VoteAnswers).SetInheritanceValidator(v => v.Add(new VoteAnswerRequestValidator()));
    }
}
