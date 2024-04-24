namespace cocktailDb.Services;

public interface ICocktailService
{
    List<Drink> GetAllDrinks();
    Drink GetDrinkById(int id);
    Drink AddDrink(Drink drink);
    Drink UpdateDrink(Drink drink);
    bool DeleteDrink(int id);
    List<Drink> GetDrinkByCategory(string category);
    List<Drink> GetDrinkByGlass(string glass);
    List<Drink> GetDrinkByAlcoholic(bool alcoholic);
    List<Glass> GetAllGlasses();
    Glass GetGlassById(int id);
    Glass AddGlass(Glass glass);
    bool DeleteGlass(int id);
    List<Ingredient> GetAllIngredients();
    Ingredient GetIngredientById(int id);
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
    public List<Drink> GetAllDrinks()
    {
        return _drinkRepository.GetAllDrinks();
    }

    //get drink by id
    public Drink GetDrinkById(int id)
    {
        return _drinkRepository.GetDrinkById(id);
    }

    //add a drink
    public Drink AddDrink(Drink drink)
    { 
        return _drinkRepository.AddDrink(drink);
    }

    //update a drink
    public Drink UpdateDrink(Drink drink)
    {
        return _drinkRepository.UpdateDrink(drink);
    }

    //delete a drink
    public bool DeleteDrink(int id)
    {
        return _drinkRepository.DeleteDrink(id);
    }

    //GetDrinkByCategory
    public List<Drink> GetDrinkByCategory(string category)
    {
        return _drinkRepository.GetDrinkByCategory(category);
    }

    //GetDrinkByGlass
    public List<Drink> GetDrinkByGlass(string glass)
    {
        return _drinkRepository.GetDrinkByGlass(glass);
    }

    //GetDrinkByAlcoholic
    public List<Drink> GetDrinkByAlcoholic(bool alcoholic)
    {
        return _drinkRepository.GetDrinkByAlcoholic(alcoholic);
    }

    //get all glasses
    public List<Glass> GetAllGlasses()
    {
        return _glassRepository.GetAllGlasses();
    }

    //get glass by id
    public Glass GetGlassById(int id)
    {
        return _glassRepository.GetGlassById(id);
    }

    //add a glass
    public Glass AddGlass(Glass glass)
    {
        return _glassRepository.AddGlass(glass);
    }

    //delete a glass
    public bool DeleteGlass(int id)
    {
        return _glassRepository.DeleteGlass(id);
    }

    //get all ingredients
    public List<Ingredient> GetAllIngredients()
    {
        return _ingredientRepository.GetAllIngredients();
    }

    //get ingredient by id
    public Ingredient GetIngredientById(int id)
    {
        return _ingredientRepository.GetIngredientById(id);
    }

}