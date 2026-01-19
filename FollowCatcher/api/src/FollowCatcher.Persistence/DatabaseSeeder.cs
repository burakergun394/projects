using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using Microsoft.EntityFrameworkCore;

namespace FollowCatcher.Persistence;

public class DatabaseSeeder(ApplicationDbContext context)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {

        if (!await context.InstagramTrackedAccounts.AnyAsync(x => x.Username == "follow_catcher", cancellationToken))
        {
            var account = new InstagramTrackedAccount("follow_catcher");
            await context.InstagramTrackedAccounts.AddAsync(account, cancellationToken);
        }

        if (!await context.InstagramTrackedAccounts.AnyAsync(x => x.Username == "cristiano", cancellationToken))
        {
            var account = new InstagramTrackedAccount("cristiano");
            await context.InstagramTrackedAccounts.AddAsync(account, cancellationToken);
        }

        if (!await context.InstagramTrackedAccounts.AnyAsync(x => x.Username == "eyupustunn", cancellationToken))
        {
            var account = new InstagramTrackedAccount("eyupustunn");
            await context.InstagramTrackedAccounts.AddAsync(account, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
