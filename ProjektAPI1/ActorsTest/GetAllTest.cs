
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI1.Controllers;
using ProjektAPI1.Data;
using ProjektAPI1.Enitites;

namespace ActorsTests
{
    public class ActorsGetAllTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly ActorController _controller;

        public ActorsGetAllTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new ActorController(_context);

            _context.actors.AddRange(
                new Actor { FirstName = "Jan", LastName = "Kowalski" },
                new Actor { FirstName = "Paweł", LastName = "Nowak" }
            );
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllRefuels_ReturnsAllRefuels()
        {

            var result = await _controller.GetAllActors();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuels = Assert.IsType<List<Actor>>(okResult.Value);
            var expectedCount = await _context.actors.CountAsync();
            Assert.Equal(expectedCount, refuels.Count);
        }
    }
}