using ZstdSharp.Unsafe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CocktailContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CocktailDbConnection")));

builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<IGlassRepository, GlassRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<ICocktailService, CocktailService>();

var app = builder.Build();

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
app.MapGet("/drinks", (ICocktailService service) =>
{
    var drinks = service.GetAllDrinks();
    return Results.Ok(drinks);
});

//get drink by id
app.MapGet("/drinks/{id}", (ICocktailService service, int id) =>
{
    var drink = service.GetDrinkById(id);
    return Results.Ok(drink);
});

//add a drink
app.MapPost("/drinks", (ICocktailService service, Drink drink) =>
{
    try
    {
        var newDrink = service.AddDrink(drink);
        //return code 201 CREATED
        return Results.Created($"/drinks/{newDrink.Id}", newDrink);
    }
    catch (System.Exception)
    {
        Console.WriteLine("There was an error adding the drink");
        throw;
    }
    
});

//update a drink
app.MapPut("/drinks", (ICocktailService service, Drink drink) =>
{
    var updatedDrink = service.UpdateDrink(drink);
    return updatedDrink;
});

//delete a drink
app.MapDelete("/drinks/{id}", (ICocktailService service, int id) =>
{
    var result = service.DeleteDrink(id);
    return result;
});

//GetDrinkByCategory
app.MapGet("/drinks/category/{category}", (ICocktailService service, string category) =>
{
    var drinks = service.GetDrinkByCategory(category);
    return drinks;
});

//GetDrinkByGlass
app.MapGet("/drinks/glass/{glass}", (ICocktailService service, string glass) =>
{
    var drinks = service.GetDrinkByGlass(glass);
    return drinks;
});

//GetDrinkByAlcoholic
app.MapGet("/drinks/alcoholic/{alcoholic}", (ICocktailService service, bool alcoholic) =>
{
    var drinks = service.GetDrinkByAlcoholic(alcoholic);
    return drinks;
});

//get all glasses
app.MapGet("/glasses", (ICocktailService service) =>
{
    var glasses = service.GetAllGlasses();
    return glasses;
});

//get glass by id
app.MapGet("/glasses/{id}", (ICocktailService service, int id) =>
{
    var glass = service.GetGlassById(id);
    return glass;
});

//add a glass
app.MapPost("/glasses", (ICocktailService service, Glass glass) =>
{
    var newGlass = service.AddGlass(glass);
    return newGlass;
});

//delete a glass
app.MapDelete("/glasses/{id}", (ICocktailService service, int id) =>
{
    var result = service.DeleteGlass(id);
    return result;
});

//get all ingredients
app.MapGet("/ingredients", (ICocktailService service) =>
{
    var ingredients = service.GetAllIngredients();
    return ingredients;
});

//get ingredient by id
app.MapGet("/ingredients/{id}", (ICocktailService service, int id) =>
{
    var ingredient = service.GetIngredientById(id);
    return ingredient;
});





app.Run("http://localhost:5000");
