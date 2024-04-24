namespace cocktailDb.Models;

public class Measurement
{
    public int Id { get; set; }

    public string DbDrinkId { get; set; }
    [ForeignKey("Drink")]
    public int IdOfDrink { get; set; }
    // Navigation property for the Drink entity
    public Drink Drink { get; set; }
    private string? _measure1;
    private string? _measure2;
    private string? _measure3;
    private string? _measure4;
    private string? _measure5;

    [JsonProperty("strMeasure1")]
    public string? Measure1 
    { 
        get => _measure1;
        set => _measure1 = value ?? "";
    }

    [JsonProperty("strMeasure2")]
    public string? Measure2 
    { 
        get => _measure2;
        set => _measure2 = value ?? "";
    }

    [JsonProperty("strMeasure3")]
    public string? Measure3 
    { 
        get => _measure3;
        set => _measure3 = value ?? "";
    }

    [JsonProperty("strMeasure4")]
    public string? Measure4 
    { 
        get => _measure4;
        set => _measure4 = value ?? "";
    }

    [JsonProperty("strMeasure5")]
    public string? Measure5 
    { 
        get => _measure5;
        set => _measure5 = value ?? "";
    }
}
