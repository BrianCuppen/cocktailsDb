using Asp.Versioning.Builder;

public static class MyGroups
{ 
    public static RouteGroupBuilder GroupDrinks(this RouteGroupBuilder builder, ApiVersionSet versionSet, ICocktailService service)
    {
        builder.MapGet("", async () =>
        {
            return Results.Ok(await service.GetAllDrinksAsync());
        });

        builder.MapGet("/v1/{id}", async (int id) =>
        {
            return Results.Ok(await service.GetDrinkByIdAsync(id));
        });

        builder.MapGet("/v2/{id}", async (int id, IMapper mapper) =>
        {
            var mapped = mapper.Map<DrinkDTO>(await service.GetDrinkByIdAsync(id), opts =>
            {
                opts.Items["GlassType"] = service.GetGlassByIdAsync(id);
                opts.Items["IngredientList"] = service.GetIngredientByIdAsync(id);
                opts.Items["MeasurementList"] = service.GetMeasurementByIdAsync(id);
            });
            return Results.Ok(mapped);
        }).WithApiVersionSet(versionSet).MapToApiVersion(2.0);

        builder.MapGet("/category", async () =>
        {
            return Results.Ok(await service.GetCategoriesAsync());
        });

        builder.MapGet("/category/{category}", async (string category) =>
        {
            return Results.Ok(await service.GetDrinkByCategoryAsync(category));
        });

        builder.MapPost("/", async (Drink drink) =>
        {
            return Results.Created("Drink added to database", await service.AddDrinkAsync(drink));
        });

        builder.MapPut("/", async (Drink drink) =>
        {
            return Results.Ok(await service.UpdateDrinkAsync(drink));
        });

        builder.MapDelete("/{id}", async (int id) =>
        {
            await service.DeleteDrinkAsync(id);
            return Results.Ok("Drink deleted.");
        });

        return builder;
    }

}
