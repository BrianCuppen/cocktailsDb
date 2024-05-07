using System.Net;
using System.Text;
using CocktailDb.Models;
using FluentAssertions;
using Newtonsoft.Json;
using tests.Factories;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace test;
[Collection("IntegrationTest")]
public class DrinkIntegrationTest
{
    [Fact]
    public async Task Get_Drinks()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        var response = await client.GetAsync("/drinks");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Drink_By_Id()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Specify the drink ID you want to retrieve
        int drinkId = 1;

        var response = await client.GetAsync($"/drinks/{drinkId}?api-version=1.0");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
    public async Task Get_Drink_By_Id2()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Specify the drink ID you want to retrieve
        int drinkId = 1;

        var response = await client.GetAsync($"/drinks/{drinkId}?api-version=2.0");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Drink_By_Category()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Specify the category you want to retrieve drinks for
        string category = "Cocktail";

        var response = await client.GetAsync($"/drinks/category/{category}");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Drink_By_Alcoholic()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Specify the alcoholic type you want to retrieve drinks for
        bool alcoholic = true;

        var response = await client.GetAsync($"/drinks/alcoholic/{alcoholic}");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Drink_By_Glass()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Specify the glass type you want to retrieve drinks for
        string glass = "Highball glass";

        var response = await client.GetAsync($"/drinks/glasses/{glass}");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task Delete_Drink()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Specify the ID of the drink to delete
        int drinkIdToDelete = 1;

        var response = await client.DeleteAsync($"/drinks/{drinkIdToDelete}");

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}