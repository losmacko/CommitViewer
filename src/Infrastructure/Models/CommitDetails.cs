namespace CommitViewer.Infrastructure.VersionControlSystem;

public class CommitDetails
{
    public Author Author { get; set; } = new();
    public string Message { get; set; } = string.Empty;
}
