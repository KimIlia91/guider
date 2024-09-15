using Guider.Application.Common;
using Guider.Domain.Categories;
using Guider.Domain.Tags;
using Guider.Domain.Venues;
using Guider.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guider.Infrastructure.Persistence;

internal static class PersistenceConfig
{
    public static void AddPersistence(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        service.AddScoped<ITagRepository, TagRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IVenueRepository, VenueRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void UsePersistence(this WebApplication app)
    {
        app.UseMigrate();
    }
}