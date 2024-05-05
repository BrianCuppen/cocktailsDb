namespace CocktailDb.Contexts;

public class CocktailContext : DbContext
{
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Glass> Glasses { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Category> Categories { get; set; }


    public CocktailContext(DbContextOptions<CocktailContext> options) : base(options)
    {
    }
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Drink>()
        .HasKey(d => d.Id);

    modelBuilder.Entity<Glass>()
        .HasKey(g => g.Id);

    modelBuilder.Entity<Ingredient>()
        .HasKey(i => i.Id);

    modelBuilder.Entity<Drink>()
        .HasOne(d => d.GlassType)
        .WithOne()
        .HasForeignKey<Drink>(d => d.GlassTypeId);

    modelBuilder.Entity<Drink>()
        .HasOne(d => d.Ingredient)
        .WithOne()
        .HasForeignKey<Drink>(d => d.IngredientId);

    modelBuilder.Entity<Drink>()
        .HasOne(d => d.Measurement)
        .WithOne()
        .HasForeignKey<Drink>(d => d.MeasurementId);

    modelBuilder.Entity<Email>()
        .HasKey(e => e.Id);

    modelBuilder.Entity<Category>()
        .HasKey(c => c.Id);
}

    //seeding
        public void SeedData()
        {
            if (!Drinks.Any())
            {
                var drinks = new List<Drink>
                {
                    new Drink
                    {
                        DbDrinkId = "11007",
                        Name = "Margarita",
                        AlternateName = "Tommy's Margarita",
                        Category = "Cocktail",
                        Iba = "Contemporary Classics",
                        Alcoholic = true,
                        Instructions = "Rub the rim of the glass with the lime slice to make the salt stick to it. Take care to moisten only the outer rim and sprinkle the salt on it. The salt should present to the lips of the imbiber and never mix into the cocktail. Shake the other ingredients with ice, then carefully pour into the glass.",
                        ImageUrl = "https://www.thecocktaildb.com/images/media/drink/wpxpvu1439905379.jpg",
                        GlassType = new Glass
                        {
                            Name = "Cocktail glass"
                        },
                        Ingredient = new Ingredient
                        {
                            IdOfDrink = 1,
                            DrinkName = "Margarita",
                            Ingredient1 = "Tequila",
                            Ingredient2 = "1 1/2 oz"
                        },
                        Measurement = new Measurement
                        {
                            IdOfDrink = 1,
                            DrinkName = "Margarita",
                            Measure1 = "Triple sec",
                            Measure2 = "1/2 oz"
                        }
                    },
                    new Drink
                    {
                        DbDrinkId = "11000",
                        Name = "Mojito",
                        Category = "Cocktail",
                        Iba = "Contemporary Classics",
                        Alcoholic = true,
                        Instructions = "Muddle mint leaves with sugar and lime juice. Add a splash of soda water and fill the glass with cracked ice. Pour the rum and top with soda water. Garnish and serve with straw.",
                        ImageUrl = "https://www.thecocktaildb.com/images/media/drink/3z6xdi1589574603.jpg",
                        AlternateName="totaly different name",
                        GlassType = new Glass
                        {
                            Name = "Highball glass"
                        },
                        Ingredient = new Ingredient
                        {
                            IdOfDrink = 2,
                            DrinkName = "Mojito",
                            Ingredient1 = "Light rum",
                            Ingredient2 = "2-3 oz"
                        },
                        Measurement = new Measurement
                        {
                            IdOfDrink = 2,
                            DrinkName = "Mojito",
                            Measure1 = "Lime",
                            Measure2 = "Juice of 1"
                        }
                    },
                    new Drink
                    {
                        DbDrinkId = "11001",
                        Name = "Old Fashioned",
                        Category = "Cocktail",
                        Iba = "Unforgettables",
                        Alcoholic = true,
                        Instructions = "Place sugar cube in old fashioned glass and saturate with bitters, add a dash of plain water. Muddle until dissolved. Fill the glass with ice cubes and add whiskey. Garnish with orange twist, and a cocktail cherry.",
                        ImageUrl = "https://www.thecocktaildb.com/images/media/drink/vrwquq1478252802.jpg",
                        AlternateName="totaly different name",
                        GlassType = new Glass
                        {
                            Name = "Old-fashioned glass"
                        },
                        Ingredient = new Ingredient
                        {
                            IdOfDrink = 3,
                            DrinkName = "Old Fashioned",
                            Ingredient1 = "Bourbon",
                            Ingredient2 = "4.5 cL"
                        },
                        Measurement = new Measurement
                        {
                            IdOfDrink = 3,
                            DrinkName = "Old Fashioned",
                            Measure1 = "Angostura bitters",
                            Measure2 = "2 dashes"
                        }
                    }
                };
                Drinks.AddRange(drinks);
                SaveChanges();
        }
        if (!Emails.Any())
        {
            var emails = new List<Email>
            {
                new Email
                {
                    EmailAdress = "JhonDoe@gmail.com",
                    EmailsSent = 0
                },
                new Email
                {
                    EmailAdress = "WilsonDoe@gmail.com",
                    EmailsSent = 2
            }
            };
            Emails.AddRange(emails);
            SaveChanges();
        }
    }
}