
using CommitViewer.Domain.Common;

namespace CommitViewer.Infrastructure.VersionControlSystem;

public class Commit : BaseEntity
{
    public string Sha { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Message { get; set; } = string.Empty;
}
