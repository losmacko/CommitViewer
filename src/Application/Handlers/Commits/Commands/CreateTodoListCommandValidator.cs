using CommitViewer.Application.Common.Interfaces;

namespace CommitViewer.Application.Handlers.Commits.Commands;

public class CreateTodoListCommandValidator : AbstractValidator<AddCommitsCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        /*
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
        */
    }
    /*
    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.TodoLists.AllAsync(l => l.Title != title, cancellationToken);
    }
    */
}
