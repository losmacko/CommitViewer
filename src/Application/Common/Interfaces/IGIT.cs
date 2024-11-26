namespace CommitViewer.Infrastructure.VersionControlSystem;

public interface IGIT
{
    Task<IEnumerable<Commit>> GetCommitsForRepositoryAsync(string username, string repository);
}
