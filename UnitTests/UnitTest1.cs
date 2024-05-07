using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using CocktailDb.Models;
using CocktailDb.Services;
using CocktailDb.Repositories;
using CocktailDb.Contexts;

namespace YourNamespace.Tests
{
    public class DrinkServiceTests
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly CocktailService _service;
        private readonly CocktailContext _context;

        public DrinkServiceTests()
        {
            _context = new CocktailContext();
            _drinkRepository = new DrinkRepository(_context);
            _service = new CocktailService(_drinkRepository);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Add_New_Drink_Failure(Drink drink)
        {
            // Act
            var result = await _service.AddDrinkAsync(drink);

            // Assert
            Assert.False(result);
        }
        
    }
}
