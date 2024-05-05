namespace CocktailDb.DTO;
    public class DrinkDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Glass GlassType { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public bool Alcoholic { get; set; }
        public Ingredient IngredientList { get; set; }
        public Measurement MeasurementList { get; set; }
    }