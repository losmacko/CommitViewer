using System.Net.Http.Headers;
using System.Text.Json;
using CommitViewer.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace CommitViewer.Infrastructure.VersionControlSystem;

public class GIT : IGIT
{
    private readonly GitOptions _gitCfg;
    private readonly HttpClient _httpClient;

    public GIT(IOptions<GitOptions> gitOptions, HttpClient httpClient)
    {
        _gitCfg = gitOptions.Value;
        var authorization = new AuthenticationHeaderValue("Bearer", "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");

        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_gitCfg.AuthorizationToGitHubName);
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
    }

    public async Task<IEnumerable<Commit>> GetCommitsForRepositoryAsync(string username, string repository)
    {
        try
        {
            var response = await _httpClient.GetAsync(_gitCfg.GetCommitsListUrl(username, repository));
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch commits: {response.ReasonPhrase}");
            }

            return Map(await DeserializeResponse(response));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private static async Task<List<CommitApiResponse>> DeserializeResponse(HttpResponseMessage response)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var repoCommits = JsonSerializer.Deserialize<List<CommitApiResponse>>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Guard.Against.Null(repoCommits, "Response is empty");
        return repoCommits;
    }

    private static IEnumerable<Commit> Map(List<CommitApiResponse> commitApiResponses)
    {
        var commits = new List<Commit>();
        foreach (var apiCommit in commitApiResponses)
        {
            commits.Add(new Commit
            {
                Sha = apiCommit.Sha,
                AuthorName = apiCommit.Commit.Author.Name,
                Date = apiCommit.Commit.Author.Date,
                Message = apiCommit.Commit.Message
            });
        }
        return commits;
    }
}
