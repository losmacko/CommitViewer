namespace CommitViewer.Infrastructure.Configuration;
public class GitOptions
{
    public const string Name = "GitConfiguration";
    public string Url { get; set; } = string.Empty;
    public string AuthorizationToGitHubName { get; set; } = string.Empty;
    public string PersonalAccessToken { get; set; } = string.Empty;

    public string GetCommitsListUrl(string userName, string repository)
    {
        return $"{Url}/{userName}/{repository}/commits";
    }
}
