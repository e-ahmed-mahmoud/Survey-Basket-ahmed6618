using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Entities;

public class Vote : AuditLogging
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int PollId { get; set; }

    public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
    public Poll Poll { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

    public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];
}
