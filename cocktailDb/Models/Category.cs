namespace cocktailDb.Models;

public class Category
{
    [JsonProperty("strCategory")]
    public string? Name { get; set; }
    [Key]
    public int Id { get; set; }
}

public class CategoryList
{
    public List<Category> Categories { get; set; }
}