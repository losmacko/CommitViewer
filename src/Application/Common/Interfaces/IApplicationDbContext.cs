using CommitViewer.Infrastructure.VersionControlSystem;

namespace CommitViewer.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Commit> Commits { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
