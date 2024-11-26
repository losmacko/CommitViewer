using CommitViewer.Application.Handlers.Commits.Commands;
using CommitViewer.Application.Handlers.Commits.Queries.GetCommits;
using CommitViewer.Web;
using Microsoft.Extensions.DependencyInjection;


public class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("you need to pass repository name and user name in format: commitViewer.exe repository user");
            Console.WriteLine("Tap any key to exit");
            Console.ReadKey();
            return;
        }
        var hostBuilder = AppHostBuilder.CreateAndBuild(args);
        var getCommitsQuery = new GetCommitsQuery
        {
            Repository = args[0],
            User = args[1]
        };

        try
        {
            var sender = hostBuilder.Services.GetRequiredService<ISender>();
            var commits = await sender.Send(getCommitsQuery);
            await sender.Send(new AddCommitsCommand { Commits = commits });

            Console.WriteLine("List of commits in format: {repository}/{Sha}: {message} {User}");
            foreach (var commit in commits)
            {
                Console.WriteLine($"{getCommitsQuery.Repository}/{commit.Sha}: {commit.Message} {getCommitsQuery.User}");
            }

            Console.WriteLine("Tap any key to exit");
            _ = Console.ReadKey(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Tap any key to exit");
            _ = Console.ReadKey(true);
        }
    }
}
