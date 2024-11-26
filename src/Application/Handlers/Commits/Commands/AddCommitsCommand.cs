using CommitViewer.Application.Common.Interfaces;
using CommitViewer.Infrastructure.VersionControlSystem;

namespace CommitViewer.Application.Handlers.Commits.Commands;

public record AddCommitsCommand : IRequest<int>
{
    public required IEnumerable<Commit> Commits { get; init; }
}

public class AddCommitsCommandHandler : IRequestHandler<AddCommitsCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddCommitsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddCommitsCommand request, CancellationToken cancellationToken)
    {
        var shaList = request.Commits.Select(x => x.Sha).ToList();
        var commitsToExclude = await _context.Commits.Where(x => shaList.Contains(x.Sha)).Select(x => x.Sha).ToListAsync();
        var commitsToAdd = request.Commits.Where(x => !commitsToExclude.Contains(x.Sha));
        
        if (commitsToAdd.Any())
        {
            _context.Commits.AddRange(commitsToAdd);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return 2;
    }
}
