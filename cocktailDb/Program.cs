using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<CocktailContext>(options =>
{
    var config = builder.Configuration.GetSection("ConnectionStrings").Get<DbConfig>();
    options.UseMySQL(config.Server);
});

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DrinkValidator>());

builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<IGlassRepository, GlassRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<ICocktailService, CocktailService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

//exception
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerFeature>()
        ?.Error;
    if (exception is not null)
    {
        var response = new { error = exception.Message };
        context.Response.StatusCode = 400;

        await context.Response.WriteAsJsonAsync(response);
    }
}));


//seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CocktailContext>();
    context.Database.EnsureCreated();
    context.SeedData();

    var glassRepository = services.GetRequiredService<IGlassRepository>();
    glassRepository.AddGlasses();

    var drinkRepository = services.GetRequiredService<IDrinkRepository>();
    drinkRepository.AddCategories();
}


app.MapGet("/", () => "Hello Backend!");

//get all drinks
app.MapGet("/drinks", async (ICocktailService service) =>
{
    return Results.Ok(await service.GetAllDrinksAsync());  
});


//get drink by id
app.MapGet("/drinks/{id}", async (ICocktailService service, int id) =>
{

    return Results.Ok(await service.GetDrinkByIdAsync(id));
});

//add a drink
app.MapPost("/drinks", async (IValidator<Drink> validator, ICocktailService service, Drink drink) =>
{
    var validationResult = validator.Validate(drink);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
    return Results.Created("Drink added to database",await service.AddDrinkAsync(drink)); // normally code 201 CREATED
});

//update a drink (doesnt really update for some reason)
app.MapPut("/drinks", async (ICocktailService service, Drink drink) =>
{
    return Results.Ok(await service.UpdateDrinkAsync(drink));
});

//delete a drink
app.MapDelete("/drinks/{id}", async (ICocktailService service, int id) =>
{
    await service.DeleteDrinkAsync(id);
    return Results.Ok("Drink deleted.");
});

//GetCategory
app.MapGet("/drinks/category", async (ICocktailService service) =>
{
    return Results.Ok(await service.GetCategoriesAsync());
});

//GetDrinkByCategory
app.MapGet("/drinks/category/{category}", async (ICocktailService service, string category) =>
{
    return Results.Ok(await service.GetDrinkByCategoryAsync(category));
});

//GetAllGlassesAsync
app.MapGet("/glasses", async (ICocktailService service) =>
{
    return Results.Ok(await service.GetAllGlassesAsync());
});

//GetDrinkByGlass NOT OK
app.MapGet("/drinks/glass/{glass}", async (ICocktailService service, string glass) =>
{
    return Results.Ok(await service.GetDrinkByGlassAsync(glass));
});

//GetDrinkByAlcoholic (alcoholic)
app.MapGet("/alcoholicDrinks/{alcoholic}", async (ICocktailService service, string alcoholic) =>
{
    if (alcoholic.ToLower() == "alcoholic")
    {
        return Results.Ok(await service.GetDrinksByAlcoholicAsync(true));
    }
    else
    {
        return Results.Ok(await service.GetDrinksByAlcoholicAsync(false));
    }
});

//get glass by id
app.MapGet("/glasses/{id}", (ICocktailService service, int id) =>
{
    var glass = service.GetGlassByIdAsync(id);
    return glass;
});

//add a glass
app.MapPost("/glasses", (ICocktailService service, Glass glass) =>
{
    var newGlass = service.AddGlassAsync(glass);
    return newGlass;
});

//delete a glass
app.MapDelete("/glasses/{id}", async (ICocktailService service, int id) =>
{
    await service.DeleteGlassAsync(id);
    return Results.Ok("Glass deleted.");
});


//get all ingredients
app.MapGet("/ingredients", async (ICocktailService service) =>
{
    return Results.Ok(await service.GetAllIngredientsAsync());
});

//get ingredient by id
app.MapGet("/ingredients/{id}", async (ICocktailService service, int id) =>
{
    return Results.Ok(await service.GetIngredientByIdAsync(id));
});


app.Run("http://localhost:5000");
