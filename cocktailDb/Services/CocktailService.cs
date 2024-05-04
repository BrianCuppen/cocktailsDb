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
    Task<IEnumerable<Drink>> GetDrinkByCategoryAsync(string category);
    Task<IEnumerable<Glass>> GetAllGlassesAsync();
    Task<IEnumerable<Drink>> GetDrinkByGlassAsync(string glass);
    Task<Glass> GetGlassByIdAsync(int id);
    Task<Glass> AddGlassAsync(Glass glass);
    Task DeleteGlassAsync(int id);
    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task<Ingredient> GetIngredientByIdAsync(int id);
    Task<IEnumerable<Drink>> GetDrinksByAlcoholicAsync(bool alcoholic);
}
public class CocktailService : ICocktailService
{
    private readonly IDrinkRepository _drinkRepository;
    private readonly IGlassRepository _glassRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMeasurementRepository _measurementRepository;

    public CocktailService(IDrinkRepository drinkRepository, IGlassRepository glassRepository, IIngredientRepository ingredientRepository, IMeasurementRepository measurementRepository)
    {
        _drinkRepository = drinkRepository;
        _glassRepository = glassRepository;
        _ingredientRepository = ingredientRepository;
        _measurementRepository = measurementRepository;
    }

    //get all drinks
    public async Task<IEnumerable<Drink>> GetAllDrinksAsync() => await _drinkRepository.GetAllDrinksAsync();

    //get drink by id
    public async Task<Drink> GetDrinkByIdAsync(int id)
    {
        var result = await _drinkRepository.GetDrinkByIdAsync(id) ?? throw new Exception("Drink not found");
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

        //check glasstype
        Glass existingGlassType = await _glassRepository.GetGlassByNameAsync(drink.GlassType.Name);

        // If the glass type doesn't exist, add it to the database
        if (existingGlassType == null)
        {
            // Set IsEdited to true
            drink.IsEdited = true;

            // Add the new glass type to the database
            drink.GlassType = await _glassRepository.AddGlassAsync(drink.GlassType);
        }
        else
        {
            // Set the drink's GlassType property to the existing glass type
            drink.GlassType = existingGlassType;
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
    public async Task<IEnumerable<Drink>> GetDrinkByCategoryAsync(string category)
    {
        var result =  await _drinkRepository.GetDrinkByCategoryAsync(category) ?? throw new Exception("No drinks found, wrong category");
        return result;
    }

    //get all glasses
    public async Task<IEnumerable<Glass>> GetAllGlassesAsync() => await _glassRepository.GetAllGlassesAsync();

    //GetDrinkByGlassAsync
    public async Task<IEnumerable<Drink>> GetDrinkByGlassAsync(string glass)
    {
        var result = await _drinkRepository.GetDrinkByGlassAsync(glass) ?? throw new Exception("No drinks found, wrong glass");
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
}