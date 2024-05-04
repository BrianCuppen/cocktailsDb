namespace cocktailDb.Models;

public class GlassResponse
{
    public Glass[] Glasses { get; set; }
}

public class Glass
{
    [Key]
    public int Id { get; set; }
    
    [JsonProperty("strGlass")]
    public string? Name { get; set; }
    [Key]

    public bool IsDeleted { get; set; } = false; // Set default value
    
}

    public class GlassList
    {
        public List<Glass> Glasses { get; set; }
    }