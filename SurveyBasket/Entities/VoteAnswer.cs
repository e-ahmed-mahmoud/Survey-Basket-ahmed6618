using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Entities;

public class VoteAnswer : AuditLogging
{
    public int Id { get; set; }

    public int VoteId { get; set; }

    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public Vote Vote { get; set; } = default!;

    public Answer Answer { get; set; } = default!;

    public Question Question { get; set; } = default!;
}
