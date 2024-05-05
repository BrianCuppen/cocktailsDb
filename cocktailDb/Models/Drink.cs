namespace CocktailDb.Models;

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
    
    
    [JsonProperty("strInstructions")]
    public string? Instructions { get; set; }
    [JsonProperty("strDrinkThumb")]
    public string? ImageUrl { get; set; }


    public Glass GlassType { get; set; }
    public Ingredient Ingredient { get; set; }
    public Measurement Measurement { get; set; }

    //IsEdited property default false
    public bool IsEdited { get; set; } = false;

    public bool IsDeleted { get; set; } = false; // Set default value

    // Navigation properties
    [ForeignKey("Glass")]
    public int GlassTypeId { get; set; }
    [ForeignKey("Ingredient")]
    public int IngredientId { get; set; }
    [ForeignKey("Measurement")]
    public int MeasurementId { get; set; }

}
