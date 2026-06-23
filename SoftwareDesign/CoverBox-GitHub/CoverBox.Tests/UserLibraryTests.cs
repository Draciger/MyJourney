using NUnit.Framework;
using CoverBox.Data_Access_Layer;
using CoverBox.Models;

namespace CoverBox.Tests
{
    [TestFixture]
    public class UserLibraryTests
    {
        private UserLibrary _userLibrary = null!;

        [SetUp]
        public void SetUp()
        {
            // Make sure the database and tables exist before each test
            DatabaseInitializer.EnsureCreated();
            _userLibrary = new UserLibrary();
        }

        [Test]
        public void Add_Then_GetById_ShouldReturnSameUser()
        {
            // Arrange
            var user = new User
            {
                Name = "Alice",
                Age = 25,
                Gender = "F",
                Country = "Norway",
                GamesPlayed = 0
            };

            // Act
            var newId = _userLibrary.Add(user);
            var loaded = _userLibrary.GetById(newId);

            // Assert
            Assert.That(loaded, Is.Not.Null);
            Assert.That(loaded!.Id, Is.EqualTo(newId));
            Assert.That(loaded.Name, Is.EqualTo(user.Name));
            Assert.That(loaded.Age, Is.EqualTo(user.Age));
            Assert.That(loaded.Gender, Is.EqualTo(user.Gender));
            Assert.That(loaded.Country, Is.EqualTo(user.Country));
            Assert.That(loaded.GamesPlayed, Is.EqualTo(user.GamesPlayed));
        }

        [Test]
        public void GetById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistingId = -1;

            // Act
            var result = _userLibrary.GetById(nonExistingId);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
