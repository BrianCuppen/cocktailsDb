namespace cocktailDb.Repositories;

public interface IGlassRepository
{
    void AddGlasses();
    List<Glass> GetAllGlasses();
    Glass GetGlassById(int id);
    Glass AddGlass(Glass glass);
    bool DeleteGlass(int id);
}
public class GlassRepository : IGlassRepository
{
    //load context
    private readonly CocktailContext _context;

    public GlassRepository(CocktailContext context)
    {
        _context = context;
    }

    //add glasses to the database trough http request www.thecocktaildb.com/api/json/v1/1/list.php?g=list
    public void AddGlasses()
    {
        //check if glasses is empty
        if (_context.Glasses.Any())
        {
            return;
        }
        try
        {
            var client = new HttpClient();
            var response = client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/list.php?g=list").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var glassList = JsonConvert.DeserializeObject<GlassList>(data);

            foreach (var glass in glassList.Glasses)
            {
                _context.Glasses.Add(glass);
            }

            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    //get all glasses
    public List<Glass> GetAllGlasses()
    {
        try
        {
            return _context.Glasses.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //get glass by id
    public Glass GetGlassById(int id)
    {
        try
        {
            return _context.Glasses.FirstOrDefault(g => g.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //add a glass
    public Glass AddGlass(Glass glass)
    {
        try
        {
            _context.Glasses.Add(glass);
            _context.SaveChanges();
            return glass;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //delete a glass
    public bool DeleteGlass(int id)
    {
        try
        {
            var glass = _context.Glasses.FirstOrDefault(g => g.Id == id);
            _context.Glasses.Remove(glass);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}