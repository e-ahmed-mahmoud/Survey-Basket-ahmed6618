using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Contracts.Dashboard;

public record VotePerQuestionsResponse(string Content, IEnumerable<QuestionAnswerCount> SelectedAnswerCount);
