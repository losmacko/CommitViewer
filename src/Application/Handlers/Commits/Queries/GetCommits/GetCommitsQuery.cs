using CommitViewer.Application.Common.Interfaces;
using CommitViewer.Infrastructure.VersionControlSystem;

namespace CommitViewer.Application.Handlers.Commits.Queries.GetCommits;

public record GetCommitsQuery : IRequest<IEnumerable<Commit>>
{
    public string Repository { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
}

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetCommitsQuery, IEnumerable<Commit>>
{
    private readonly IGIT _git;

    public GetWeatherForecastsQueryHandler(IApplicationDbContext context, IGIT git)
    {
        _git = git;
    }

    public async Task<IEnumerable<Commit>> Handle(GetCommitsQuery request, CancellationToken cancellationToken)
    {
        var commits = await _git.GetCommitsForRepositoryAsync(request.User, request.Repository);
        return commits;
    }
}
