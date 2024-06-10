﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI1.Controllers;
using ProjektAPI1.Data;
using ProjektAPI1.Enitites;
using Xunit;

namespace ActorsTests
{
    public class ActorsUpdateTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly ActorController _controller;

        public ActorsUpdateTests()
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
        public async Task UpdateActor_UpdatesExistingActor()
        {
            // Arrange
            var existingActor = await _context.actors.FirstAsync();
            var updatedActor = new Actor
            {
                Id = existingActor.Id,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName"
            };

            // Act
            var result = await _controller.UpdateActor(updatedActor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actors = Assert.IsType<List<Actor>>(okResult.Value);
            var dbActor = await _context.actors.FindAsync(existingActor.Id);

            Assert.NotNull(dbActor);
            Assert.Equal("UpdatedFirstName", dbActor.FirstName);
            Assert.Equal("UpdatedLastName", dbActor.LastName);
        }
    }
}
