namespace CocktailDb.Repositories;

public interface IDrinkRepository
{
    void AddCategories();
    Task<IEnumerable<Drink>> GetAllDrinksAsync();
    Task<Drink> GetDrinkByIdAsync(int id);
    Task<Drink> GetDrinkByDbDrinkIdAsync(string dbDrinkId);
    Task<Drink> GetDrinkByNameAsync(string name);
    Task<Drink> AddDrinkAsync(Drink drink);
    Task<Drink> UpdateDrinkAsync(Drink drink);
    Task DeleteDrinkAsync(int id);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryByNameAsync(string name);
    Task<Category> AddCategoryAsync(string category);
    Task<List<Drink>> GetDrinkByCategoryAsync(string category);
    Task<IEnumerable<Drink>> GetDrinkByGlassAsync(string glass);
    Task<IEnumerable<Drink>> GetDrinkByAlcoholicAsync(bool alcoholic);
}
public class DrinkRepository : IDrinkRepository
{
    //load context
    private readonly CocktailContext _context;

    public DrinkRepository(CocktailContext context)
    {
        _context = context;
    }

    //AddCategories
    public void AddCategories()
    {
        //check if categories is empty
        if (_context.Categories.Any())
        {
            return;
        }
        try
        {
            var client = new HttpClient();
            var response = client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var categoryList = JsonConvert.DeserializeObject<CategoryList>(data);

            foreach (var category in categoryList.Categories)
            {
                _context.Categories.Add(category);
            }

            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }

    //get all drinks
    public async Task<IEnumerable<Drink>> GetAllDrinksAsync()
    {
        return await _context.Drinks.Where(drink => !drink.IsDeleted).ToListAsync();
    }


    //get drink by id
    public async Task<Drink> GetDrinkByIdAsync(int id)
    {
        return await _context.Drinks.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
    }

    //get drink by name
    public async Task<Drink> GetDrinkByNameAsync(string name)
    {
        return await _context.Drinks.FirstOrDefaultAsync(d => d.Name == name);
    }

    //get drink by dbDrinkId
    public async Task<Drink> GetDrinkByDbDrinkIdAsync(string dbDrinkId)
    {
        return await _context.Drinks.FirstOrDefaultAsync(d => d.DbDrinkId == dbDrinkId);
    }

    //add a drink
    public async Task<Drink> AddDrinkAsync(Drink drink)
    {
        _context.Drinks.Add(drink);
        await _context.SaveChangesAsync();
        return drink;
    }

    //update a drink
    public async Task<Drink> UpdateDrinkAsync(Drink drink)
    {
        //set IsEdited to true
        drink.IsEdited = true;
        _context.Entry(drink).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return drink;
    }

    //delete a drink
    public async Task DeleteDrinkAsync(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        //set IsDeleted to true
        drink.IsDeleted = true;
        //_context.Drinks.Remove(drink);
        await _context.SaveChangesAsync();
    }

    //GetCategories
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    //GetCategoryByName
    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name);
    }

    //AddCategory
    public async Task<Category> AddCategoryAsync(string category)
    {
        var newCategory = new Category { Name = category };
        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync();
        return newCategory;
    }


    //GetDrinkByCategory
    public async Task<List<Drink>> GetDrinkByCategoryAsync(string category)
    {
        return await _context.Drinks.Where(d => d.Category == category && !d.IsDeleted).ToListAsync();
    }


    //GetDrinkByGlass
    public async Task<IEnumerable<Drink>> GetDrinkByGlassAsync(string glass)
    {
        return await _context.Drinks.Where(d => d.GlassType.Name == glass && !d.IsDeleted).ToListAsync();
    }


    //GetDrinkByAlcoholic
    public async Task<IEnumerable<Drink>> GetDrinkByAlcoholicAsync(bool alcoholic)
    {
        return await _context.Drinks.Where(d => d.Alcoholic == alcoholic && !d.IsDeleted).ToListAsync();
    }
}