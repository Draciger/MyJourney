using NUnit.Framework;
using CoverBox.Data_Access_Layer;
using CoverBox.Models;

namespace CoverBox.Tests
{
    [TestFixture]
    public class GameLibraryTests
    {
        private GameLibrary _gameLibrary = null!;

        [SetUp]
        public void SetUp()
        {
            // Make sure the database and tables exist before each test
            DatabaseInitializer.EnsureCreated();
            _gameLibrary = new GameLibrary();
        }

        [Test]
        public void Add_Then_GetById_ShouldReturnSameGame()
        {
            // Arrange
            var game = new Game
            {
                Name = "Test Game",
                System = "PC",
                YearReleased = 2024
            };

            // Act
            var newId = _gameLibrary.Add(game);
            var loaded = _gameLibrary.GetById(newId);

            // Assert
            Assert.That(loaded, Is.Not.Null);
            Assert.That(loaded!.Id, Is.EqualTo(newId));
            Assert.That(loaded.Name, Is.EqualTo(game.Name));
            Assert.That(loaded.System, Is.EqualTo(game.System));
            Assert.That(loaded.YearReleased, Is.EqualTo(game.YearReleased));
        }

        [Test]
        public void GetById_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            var nonExistingId = -1;

            // Act
            var result = _gameLibrary.GetById(nonExistingId);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
