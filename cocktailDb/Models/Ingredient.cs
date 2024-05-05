namespace CocktailDb.Models;
public class Ingredient
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Drink")]
    public int IdOfDrink { get; set; }

    [JsonProperty("strDrink")]
    public string DrinkName { get; set; }

    private string? _ingredient1;
    private string? _ingredient2;
    private string? _ingredient3;
    private string? _ingredient4;
    private string? _ingredient5;

    [JsonProperty("strIngredient1")]
    public string? Ingredient1
    { 
        get => _ingredient1;
        set => _ingredient1 = value ?? "";
    }

    [JsonProperty("strIngredient2")]
    public string? Ingredient2 
    { 
        get => _ingredient2;
        set => _ingredient2 = value ?? "";
    }

    [JsonProperty("strIngredient3")]
    public string? Ingredient3 
    { 
        get => _ingredient3;
        set => _ingredient3 = value ?? "";
    }

    [JsonProperty("strIngredient4")]
    public string? Ingredient4 
    { 
        get => _ingredient4;
        set => _ingredient4 = value ?? "";
    }

    [JsonProperty("strIngredient5")]
    public string? Ingredient5 
    { 
        get => _ingredient5;
        set => _ingredient5 = value ?? "";
    }

    public bool IsDeleted { get; set; }
}
