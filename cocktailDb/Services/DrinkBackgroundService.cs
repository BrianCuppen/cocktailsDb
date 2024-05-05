namespace CocktailDb.Services;

public class DrinkBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DrinkBackgroundService> _logger;

    public DrinkBackgroundService(IServiceProvider serviceProvider, ILogger<DrinkBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var cocktailService = scope.ServiceProvider.GetRequiredService<ICocktailService>();

                // Call the method from ICocktailService to check how many drinks are in the database
                var drinks = await cocktailService.GetAllDrinksAsync();
                int amountOfDrinks = drinks.Count();

                // Log the drink count
                _logger.LogInformation($"There are {amountOfDrinks} drinks currently in the database.");

                if (amountOfDrinks < 2)
                {
                    _logger.LogInformation("The amount of drinks in the database is less than 2. Caution");
                }

                // Delay for 1 minute
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
