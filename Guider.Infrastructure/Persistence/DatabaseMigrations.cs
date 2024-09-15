using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Guider.Infrastructure.Persistence;

public static class DatabaseMigrations
{
    public static void UseMigrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        NpgsqlMigrate(context);
    }

    private static void NpgsqlMigrate(ApplicationDbContext context)
    {
        if (context.Database.IsNpgsql()) context.Database.Migrate();
    }
}