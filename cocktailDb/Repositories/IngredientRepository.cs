namespace cocktailDb.Repositories;

public interface IIngredientRepository
{
    List<Ingredient> GetAllIngredients();
    Ingredient GetIngredientById(int id);
}
public class IngredientRepository : IIngredientRepository
{
    //load context
    private readonly CocktailContext _context;

    public IngredientRepository(CocktailContext context)
    {
        _context = context;
    }

    //get all ingredients
    public List<Ingredient> GetAllIngredients()
    {
        try
        {
            return _context.Ingredients.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //get ingredient by id
    public Ingredient GetIngredientById(int id)
    {
        try
        {
            return _context.Ingredients.FirstOrDefault(i => i.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

}
