using CocktailDb.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace tests.Factories;

public class DrinkFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<CocktailContext>));
            services.AddDbContext<CocktailContext>(options => options.UseMySQL("Server=127.0.0.1;Database=cocktaildb;Uid=root;Pwd=QNewx-72;"));

            var context = CreateContext(services);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //Wait for database to be created
            Thread.Sleep(2000);
        });

        base.ConfigureWebHost(builder);
    }

        private static CocktailContext CreateContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CocktailContext>();
        return context;
    }
}