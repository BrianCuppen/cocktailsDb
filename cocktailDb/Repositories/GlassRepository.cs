namespace cocktailDb.Repositories;

public interface IGlassRepository
{
    void AddGlasses();
    Task<List<Glass>> GetAllGlassesAsync();
    Task<Glass> GetGlassByIdAsync(int id);
    Task<Glass> GetGlassByNameAsync(string name);
    Task<Glass> AddGlassAsync(Glass glass);
    Task<bool> DeleteGlassAsync(int id);
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
    public async Task<List<Glass>> GetAllGlassesAsync() => await _context.Glasses.Where(g => !g.IsDeleted).ToListAsync();

    //get glass by id
    public async Task<Glass> GetGlassByIdAsync(int id)
    {
        return await _context.Glasses.FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
    }

    //get glass by name
    public async Task<Glass> GetGlassByNameAsync(string name)
    {
        return await _context.Glasses.FirstOrDefaultAsync(g => g.Name == name);
    }

    //add a glass
    public async Task<Glass> AddGlassAsync(Glass glass)
    {
        _context.Glasses.Add(glass);
        await _context.SaveChangesAsync();
        return glass;
    }

    //delete a glass
    public async Task<bool> DeleteGlassAsync(int id)
    {
        var glass = _context.Glasses.FirstOrDefault(g => g.Id == id);
        //set IsDeleted to true
        glass.IsDeleted = true;
        //_context.Glasses.Remove(glass);
        await _context.SaveChangesAsync();
        return true;

    }
    
}