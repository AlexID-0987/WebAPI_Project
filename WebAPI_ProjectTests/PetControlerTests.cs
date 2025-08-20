using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using WebAPI_Project.Controllers;
using WebAPI_Project.Models;

namespace WebAPI_ProjectTests
{
    [TestFixture]
    public class PetControllerTests
    {
        private readonly PetController _controller;
        private PetDbContext _context;
        private Mock<PetDbContext> _mockContext;
        [SetUp]
        public void Setup()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<PetDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
            _context = new PetDbContext(options);
            _context.Database.EnsureCreated();
            _context.Pets.Add(new Pet { Id = 1, Type = "Dog", Name = "Lumpy" });
            _context.Pets.Add(new Pet { Id = 2, Type = "Cat", Name = "Li" });
            _context.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        //check return the list of pets
        public void ReturnAllPets()
        {
            List<Pet> pets = _context.Pets.ToList();
            Assert.That(pets.Count, Is.EqualTo(2));
            Assert.That(pets[0].Name, Is.EqualTo("Lumpy"));
            Assert.That(pets[1].Name, Is.EqualTo("Li"));

        }
        [Test]
        //check return list is not null
        public void ToListIsNotNull()
        {
            List<Pet> pets = _context.Pets.ToList();
            Assert.That(pets, Is.Not.Null);
        }
        [Test]
        //check return list is empty
        public void ToListIsEmpty()
        {
            List<Pet> pets = _context.Pets.ToList();
            Assert.That(pets.Count, Is.Not.EqualTo(0));
        }
        [Test]
        //check return list is not empty
        public void ToListIsNotEmpty()
        {
            List<Pet> pets = _context.Pets.ToList();
            Assert.That(pets.Count, Is.GreaterThan(0));
        }
        [Test]
        public async Task GetAllPets_WhenDatabaseIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange: створюємо пусту базу
            var options = new DbContextOptionsBuilder<PetDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using var emptyContext = new PetDbContext(options);
            var controller = new PetController(emptyContext);

            // Act + Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await controller.GetAll());
            Assert.That(ex.Message, Is.EqualTo("Database is empty!"));
        }
        [Test]
        public async Task DeletePets_WhenDeleteItem_RemovesItFromCollection()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PetDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new PetDbContext(options);
            dbContext.Pets.Add(new Pet { Id = 1, Name = "Dog" });
            dbContext.SaveChanges();

            var sut = new PetController(dbContext);

            // Act
            var result = await sut.Delete(1);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());

            var pet = await dbContext.Pets.FindAsync(1);
            Assert.That(pet, Is.Null);
        }



    }
}