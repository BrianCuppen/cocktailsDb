using System.Net.Http.Json;
namespace CocktailDb.Services;

public interface ICocktailService
{
    Task<IEnumerable<Drink>> GetAllDrinksAsync();
    Task<HttpResponseMessage> AddDrinkAsync(Drink drink);
    Task<Drink> GetDrinkByIdAsync(int id);
    Task<HttpResponseMessage> UpdateDrinkAsync(Drink drink);
    Task<HttpResponseMessage> DeleteDrinkAsync(int id);
}
public class CocktailService : ICocktailService
{

    private readonly HttpClient _httpClient;

    public CocktailService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        _httpClient.DefaultRequestHeaders.Add("Api-Key", "key2");
    }

    //Get all drinks
    public async Task<IEnumerable<Drink>> GetAllDrinksAsync()
    {
        var requestUri = "drinks";
        return await _httpClient.GetFromJsonAsync<IEnumerable<Drink>>(requestUri);
    }

    //Get a drink by id
    public async Task<Drink> GetDrinkByIdAsync(int id)
    {
        var requestUri = $"drinks/{id}";
        return await _httpClient.GetFromJsonAsync<Drink>(requestUri);
    }

    //Add a drink
    public async Task<HttpResponseMessage> AddDrinkAsync(Drink drink)
    {
        var requestUri = "drinks";
        return await _httpClient.PostAsJsonAsync(requestUri, drink);
    }

    //edit a drink
    public async Task<HttpResponseMessage> UpdateDrinkAsync(Drink drink)
    {
        var requestUri = "drinks";
        return await _httpClient.PutAsJsonAsync(requestUri, drink);
    }

    //delete a drink
    public async Task<HttpResponseMessage> DeleteDrinkAsync(int id)
    {
        var requestUri = $"drinks/delete/{id}";
        return await _httpClient.DeleteAsync(requestUri);
    }
}