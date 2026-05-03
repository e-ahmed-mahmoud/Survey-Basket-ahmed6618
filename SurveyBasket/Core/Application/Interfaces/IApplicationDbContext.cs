using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SurveyBasket.Core.Application.Interfaces;

/// <summary>
/// Application database context abstraction to decouple services from EF Core implementation
/// </summary>
public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<ApplicationRole> Roles { get; }
    DbSet<IdentityUserRole<string>> UserRoles { get; }
    DbSet<IdentityRoleClaim<string>> RoleClaims { get; }

    DbSet<Poll> Polls { get; }
    DbSet<Question> Questions { get; }
    DbSet<Answer> Answers { get; }

    DbSet<Vote> Votes { get; }
    DbSet<VoteAnswer> VoteAnswers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
