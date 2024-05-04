namespace cocktailDb.Repositories;

public interface IIngredientRepository
{
    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task<Ingredient> GetIngredientByIdAsync(int id);
    Task DeleteIngredientAsync(int id);
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
    public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
    {
        return await _context.Ingredients.Where(i => !i.IsDeleted).ToListAsync();
    }

    //get ingredient by id
    public async Task<Ingredient> GetIngredientByIdAsync(int id)
    {
        return await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
    }


    //delete an ingredient
    public async Task DeleteIngredientAsync(int id)
    {
        var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        ingredient.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

}
