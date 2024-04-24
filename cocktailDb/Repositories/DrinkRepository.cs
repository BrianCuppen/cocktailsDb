namespace cocktailDb.Repositories;

public interface IDrinkRepository
{
    void AddCategories();
    List<Drink> GetAllDrinks();
    Drink GetDrinkById(int id);
    Drink AddDrink(Drink drink);
    Drink UpdateDrink(Drink drink);
    bool DeleteDrink(int id);
    List<Drink> GetDrinkByCategory(string category);
    List<Drink> GetDrinkByGlass(string glass);
    List<Drink> GetDrinkByAlcoholic(bool alcoholic);
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
    public List<Drink> GetAllDrinks()
    {
        try
        {
            return _context.Drinks.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //get drink by id
    public Drink GetDrinkById(int id)
    {
        try
        {
            return _context.Drinks.FirstOrDefault(d => d.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //add a drink
    public Drink AddDrink(Drink drink)
    {
        //check if the drink name or id exists
        if (_context.Drinks.Any(d => d.Name == drink.Name || d.Id == drink.Id || d.DbDrinkId == drink.DbDrinkId))
        {
            Console.WriteLine("Drink already exists");
            return null;
        }
        try
        {
            //set IsEdited to true
            drink.IsEdited = true;
            _context.Drinks.Add(drink);
            _context.SaveChanges();
            return drink;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //update a drink
    public Drink UpdateDrink(Drink drink)
    {
        try
        {
            //set IsEdited to true
            drink.IsEdited = true;
            _context.Drinks.Update(drink);
            _context.SaveChanges();
            return drink;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //delete a drink
    public bool DeleteDrink(int id)
    {
        try
        {
            var drink = _context.Drinks.FirstOrDefault(d => d.Id == id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
                _context.SaveChanges();
                return true;
            }
            Console.WriteLine("Drink not found");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    //GetDrinkByCategory
    public List<Drink> GetDrinkByCategory(string category)
    {
        try
        {
        //check if the category exists
        if (_context.Drinks.Any(d => d.Category == category))
        {
            return _context.Drinks.Where(d => d.Category == category).ToList();
        }
        Console.WriteLine("Category not found");
        return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //GetDrinkByGlass
    public List<Drink> GetDrinkByGlass(string glass)
    {
        try
        {
        //check if the glass exists
        if (_context.Drinks.Any(d => d.GlassType.Name == glass))
        {
            return _context.Drinks.Where(d => d.GlassType.Name == glass).ToList();
        }
        Console.WriteLine("Glass not found");
        return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //GetDrinkByAlcoholic
    public List<Drink> GetDrinkByAlcoholic(bool alcoholic)
    {
        try
        {
        //check if the alcoholic exists
        if (_context.Drinks.Any(d => d.Alcoholic == alcoholic))
        {
            return _context.Drinks.Where(d => d.Alcoholic == alcoholic).ToList();
        }
        Console.WriteLine("Alcoholic not found");
        return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}