using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Contracts.Votes;

public record VoteAnswerRequest(int QuestionId, int AnswerId);
