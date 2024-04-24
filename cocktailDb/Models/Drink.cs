namespace cocktailDb.Models;

public class Drink
{
    //primary key
    [Key]
    public int Id { get; set; }
    [JsonProperty("idDrink")]
    public string DbDrinkId { get; set; }
    [JsonProperty("strDrink")]
    public string? Name { get; set; }
    private string? _alternateName;

    [JsonProperty("strDrinkAlternate")]
    public string? AlternateName 
    { 
        get => _alternateName;
        set => _alternateName = value ?? "unknown";
    }
    [JsonProperty("strCategory")]
    public string? Category { get; set; }
    [JsonProperty("strIBA")]
    public string? Iba { get; set; }
    private string? _alcoholic { get; set; }
    [JsonProperty("strAlcoholic")]
    public bool Alcoholic 
    { 
        get => _alcoholic == "Alcoholic";
        set => _alcoholic = value ? "Alcoholic" : "Non Alcoholic";
    }
    [JsonProperty("strGlass")]
    public string? Glass { get; set; }
    [JsonProperty("strInstructions")]
    public string? Instructions { get; set; }
    [JsonProperty("strDrinkThumb")]
    public string? ImageUrl { get; set; }

    public Ingredient Ingredients { get; set; }

    public Measurement Measurements { get; set; }

}
