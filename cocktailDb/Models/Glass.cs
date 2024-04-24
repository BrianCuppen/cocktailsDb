namespace cocktailDb.Models;

public class GlassResponse
{
    public Glass[] Glasses { get; set; }
}

public class Glass
{
    [JsonProperty("strGlass")]
    public string? Name { get; set; }
    [Key]
    public int Id { get; set; }
}