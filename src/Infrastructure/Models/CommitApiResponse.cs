namespace CommitViewer.Infrastructure.VersionControlSystem;

public class CommitApiResponse
{
    public string Sha { get; set; } = string.Empty;
    public CommitDetails Commit { get; set; } = new();
}
