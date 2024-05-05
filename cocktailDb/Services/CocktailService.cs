using Microsoft.AspNetCore.Mvc;

namespace CocktailDb.Services;

public interface ICocktailService
{
    Task<IEnumerable<Drink>> GetAllDrinksAsync();
    Task<Drink> GetDrinkByIdAsync(int id);
    Task<Drink> GetDrinkByDbDrinkIdAsync(string dbDrinkId);
    Task<Drink> AddDrinkAsync(Drink drink);
    Task<Drink> UpdateDrinkAsync(Drink drink);
    Task DeleteDrinkAsync(int id);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<List<Drink>> GetDrinkByCategoryAsync(string category);
    Task<IEnumerable<Glass>> GetAllGlassesAsync();
    Task<IEnumerable<Drink>> GetDrinkByGlassAsync(string glass);
    Task<Glass> GetGlassByIdAsync(int id);
    Task<Glass> AddGlassAsync(Glass glass);
    Task DeleteGlassAsync(int id);
    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task<Ingredient> GetIngredientByIdAsync(int id);
    Task<IEnumerable<Drink>> GetDrinksByAlcoholicAsync(bool alcoholic);
    Task<FileStreamResult> DownloadDrinksAsync(string category);
    Task<IResult> UploadDrinkAsync(IFormFile file, IValidator<Drink> validator);
    Task<IEnumerable<Measurement>> GetAllMeasurementsAsync();
    Task<Measurement> GetMeasurementByIdAsync(int id);
}
public class CocktailService() : ICocktailService
{
    private readonly IDrinkRepository _drinkRepository;
    private readonly IGlassRepository _glassRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IMemoryCache _memoryCache;

    public CocktailService(IDrinkRepository drinkRepository, IGlassRepository glassRepository, IIngredientRepository ingredientRepository, IMeasurementRepository measurementRepository, IMemoryCache memoryCache)
        : this()
    {
        _drinkRepository = drinkRepository;
        _glassRepository = glassRepository;
        _ingredientRepository = ingredientRepository;
        _measurementRepository = measurementRepository;
        _memoryCache = memoryCache;
    }

    //get all drinks
    //public async Task<IEnumerable<Drink>> GetAllDrinksAsync() => await _drinkRepository.GetAllDrinksAsync();

    public Task<IEnumerable<Drink>> GetAllDrinksAsync() // chache => if the called again it'll use what is stored in cache
    {
        return _memoryCache.GetOrCreateAsync("drinks", async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            return await _drinkRepository.GetAllDrinksAsync();
        });
    }
    //get drink by id
    public async Task<Drink> GetDrinkByIdAsync(int id)
    {
        var result = await _drinkRepository.GetDrinkByIdAsync(id) ?? throw new Exception("Drink not found");
        //get the glass, ingredient and measurement from this drink and add it to the result
        result.GlassType = await _glassRepository.GetGlassByIdAsync(result.GlassTypeId);
        result.Ingredient = await _ingredientRepository.GetIngredientByIdAsync(result.IngredientId);
        result.Measurement = await _measurementRepository.GetMeasurementByIdAsync(result.MeasurementId);
        return result;
    }

    //get drink by dbDrinkId
    public async Task<Drink> GetDrinkByDbDrinkIdAsync(string dbDrinkId)
    {
        var result = await _drinkRepository.GetDrinkByDbDrinkIdAsync(dbDrinkId) ?? throw new Exception("Drink not found");
        return result;
    }

    //add a drink
    public async Task<Drink> AddDrinkAsync(Drink drink)
    {
        // Check if the drink name already exists
        var existingDrink = await _drinkRepository.GetDrinkByNameAsync(drink.Name);
        if (existingDrink != null)
        {
            throw new Exception("Drink already exists");
        }
        // If the glass type doesn't exist, add it to the database
        if (await _glassRepository.GetGlassByNameAsync(drink.GlassType.Name) == null)
        {
            drink.GlassType = await _glassRepository.AddGlassAsync(drink.GlassType);
        }
        else
        {
            // Set the drink's GlassType property to the existing glass type
            drink.GlassType = await _glassRepository.GetGlassByNameAsync(drink.GlassType.Name);
            //set Ingredient.DrinkName
            drink.Ingredient.DrinkName = drink.Name;
            drink.Measurement.DrinkName = drink.Name;
        }
        // If the category doesn't exist, add it to the database
        if (await _drinkRepository.GetCategoryByNameAsync(drink.Category.ToLower()) == null)
        {
            await _drinkRepository.AddCategoryAsync(drink.Category);
        }
        return await _drinkRepository.AddDrinkAsync(drink);
    }


    //update a drink
    public async Task<Drink> UpdateDrinkAsync(Drink drink)
    {
        var result = await _drinkRepository.GetDrinkByNameAsync(drink.Name) ?? throw new Exception("Drink not found");
        return await _drinkRepository.UpdateDrinkAsync(drink);
    }

    //delete a drink
    public async Task DeleteDrinkAsync(int id)
    {
        var drink = await _drinkRepository.GetDrinkByIdAsync(id) ?? throw new Exception("Can't delete drink, drink not found");
        await _drinkRepository.DeleteDrinkAsync(id);
    }

    //GetCategory
    public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _drinkRepository.GetCategoriesAsync();

    //GetDrinkByCategory
    public async Task<List<Drink>> GetDrinkByCategoryAsync(string category)
    {
        List<Drink> result = await _drinkRepository.GetDrinkByCategoryAsync(category) ?? throw new Exception("No drinks found, wrong category");
        return result;
    }

    //get all glasses
    public async Task<IEnumerable<Glass>> GetAllGlassesAsync() => await _glassRepository.GetAllGlassesAsync();

    //GetDrinkByGlassAsync
    public async Task<IEnumerable<Drink>> GetDrinkByGlassAsync(string glass)
    {
        var result = await _drinkRepository.GetDrinkByGlassAsync(glass) ?? throw new Exception("No drinks found, wrong glass");
        //get the glass, ingredient and measurement from this drink and add it to the result
        foreach (var drink in result)
        {
            drink.GlassType = await _glassRepository.GetGlassByIdAsync(drink.GlassTypeId);
            drink.Ingredient = await _ingredientRepository.GetIngredientByIdAsync(drink.IngredientId);
            drink.Measurement = await _measurementRepository.GetMeasurementByIdAsync(drink.MeasurementId);
        }
        return result;
    }

    //get glass by id
    public async Task<Glass> GetGlassByIdAsync(int id)
    {
        var result = await _glassRepository.GetGlassByIdAsync(id) ?? throw new Exception("Glass not found");
        return result;
    }

    //add a glass
    public async Task<Glass> AddGlassAsync(Glass glass)
    {
        //check if glass already exists
        var existingGlass = await _glassRepository.GetGlassByNameAsync(glass.Name);
        if (existingGlass != null)
        {
            throw new Exception("Glass already exists");
        }
        return await _glassRepository.AddGlassAsync(glass);
    }

    //delete a glass
    public async Task DeleteGlassAsync(int id)
    {
        var result = await _glassRepository.GetGlassByIdAsync(id) ?? throw new Exception("Can't delete glass, glass not found");
        await _glassRepository.DeleteGlassAsync(id);
    }

    //get all ingredients
    public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync() => await _ingredientRepository.GetAllIngredientsAsync();

    //get ingredient by id
    public async Task<Ingredient> GetIngredientByIdAsync(int id)
    {
        var result = await _ingredientRepository.GetIngredientByIdAsync(id) ?? throw new Exception("Ingredient not found");
        return result;
    }

    public async Task<IEnumerable<Drink>> GetDrinksByAlcoholicAsync(bool alcoholic)
    {
        return await _drinkRepository.GetDrinkByAlcoholicAsync(alcoholic);
    }

    //download drinks
public async Task<FileStreamResult> DownloadDrinksAsync(string category)
{
    // Retrieve all Drinks from that category
    var drinks = await GetDrinkByCategoryAsync(category);

    // Create a StringBuilder to construct the CSV content
    var csvContent = new StringBuilder();

    // Append header row
    csvContent.AppendLine($"Name,AlternateName,Category,IBA,Alcoholic,GlassType");

    // Append data rows
    foreach (var drink in drinks)
    {
        // Construct the CSV row with the desired information
        var alcoholic = drink.Alcoholic ? "Alcoholic" : "Non-Alcoholic";
        var csvRow = $"{drink.Name},{drink.AlternateName ?? "N/A"},{drink.Category ?? "N/A"},{drink.Iba ?? "N/A"},{alcoholic},{drink.GlassType?.Name ?? "N/A"}";

        csvContent.AppendLine(csvRow);
    }

    // Convert the CSV content to bytes
    byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());

    // Create a MemoryStream from the CSV bytes
    var memoryStream = new MemoryStream(csvBytes);

    // Return the FileStreamResult
    return new FileStreamResult(memoryStream, "text/csv")
    {
        FileDownloadName = "drinks.csv"
    };
}

    //upload a drink
    public async Task<IResult> UploadDrinkAsync(IFormFile file, IValidator<Drink> validator)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("File not selected or empty.");
            }

            // Save the uploaded file to a local folder
            string uploadFolder = "./uploads";
            string filePath = Path.Combine(uploadFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read the content of the uploaded file
            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            // Parse the content of the file to extract drink objects
            var drinkObjects = ParseDrinkObjects(content);

            // Convert each drink object into a Drink object and store it in the database
            foreach (var drinkObject in drinkObjects)
            {
                // Convert drinkObject to Drink model
                var drink = ConvertToDrink(drinkObject);

                // Validate the Drink model
                var validationResult = validator.Validate(drink);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
                    //don't add it 
                    return Results.BadRequest(errors);
                }

                // Add the Drink to the database
                await AddDrinkAsync(drink);
            }

            return Results.Ok($"Uploaded {drinkObjects.Count} drinks successfully. File saved at: {filePath}");
        }
        catch (Exception ex)
        {
            return Results.Problem($"Internal server error: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    List<Drink> ParseDrinkObjects(string content)
    {
        var drinkObjects = new List<Drink>();

        // Split the content into lines
        var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Skip the first line (header row) if present
        var dataLines = lines.Skip(1);

        // Iterate over each line
        foreach (var line in dataLines)
        {
            // Parse the line to extract drink properties
            var properties = line.Split(',');

            // Create a new DrinkObject instance and populate its properties
            var drinkObject = new Drink
            {
                DbDrinkId = properties[0].Trim('"'),
                Name = properties[1].Trim('"'),
                AlternateName = properties[2].Trim('"'),
                Category = properties[3].Trim('"'),
                Iba = properties[4].Trim('"'),
                Alcoholic = bool.Parse(properties[5].Trim('"')), // Remove surrounding quotes before parsing
                ImageUrl = properties[6].Trim('"'),
                //Ingredients = properties[7], // Assuming ingredients are provided as a string in the CSV
                //Measurements = properties[8], // Assuming measurements are provided as a string in the CSV
                IsEdited = bool.Parse(properties[9].Trim('"')), // Remove surrounding quotes before parsing
                GlassType = new Glass { Name = properties[10].Trim('"') }
            };

            // Add the parsed drink object to the list
            drinkObjects.Add(drinkObject);
        }

        return drinkObjects;
    }

    Drink ConvertToDrink(Drink drinkObject)
    {
        // Convert DrinkObject to Drink model
        var drink = new Drink
        {
            Name = drinkObject.Name,
            AlternateName = drinkObject.AlternateName,
            Category = drinkObject.Category,
            Iba = drinkObject.Iba,
            Alcoholic = drinkObject.Alcoholic,
            ImageUrl = drinkObject.ImageUrl,
            Ingredient = drinkObject.Ingredient,
            Measurement = drinkObject.Measurement,
            IsEdited = drinkObject.IsEdited,
            GlassType = drinkObject.GlassType
        };

        return drink;
    }

    //GetAllMeasurementsAsync
    public async Task<IEnumerable<Measurement>> GetAllMeasurementsAsync() => await _measurementRepository.GetAllMeasurementsAsync();

    //GetMeasurementByIdAsync
    public async Task<Measurement> GetMeasurementByIdAsync(int id)
    {
        var result = await _measurementRepository.GetMeasurementByIdAsync(id) ?? throw new Exception("Measurement not found");
        return result;
    }
}