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
public class IntegrationTest
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
    public async Task Add_New_Drink()
    {
        var factory = new DrinkFactory();
        var client = factory.CreateClient();

        // Add your API key to the request headers
        client.DefaultRequestHeaders.Add("Api-Key", "key2");

        // Create a new Drink object to add
        Drink newDrink = new Drink
        {
            DbDrinkId = "TestId",
            Name = "IntegrationTestDrink",
            AlternateName = "Tommy's Favorite"
            // Other properties...
        };

        var response = await client.PostAsJsonAsync("/drinks/Add", newDrink);

        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}