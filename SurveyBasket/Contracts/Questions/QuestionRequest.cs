using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Contracts.Questions;

public record QuestionRequest(string Content, List<string> Answers);

