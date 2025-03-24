using PasswordForUsLibrary.PassGenerator;

namespace TestsForPasswordForUs
{
    [TestFixture]
    public class PassGeneratorTests
    {
        private PassGenerator _passGenerator;
        private string[] _categories;

        [SetUp]
        public void Setup()
        {
            _categories = new[]
            {
                "0123456789",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "abcdefghijklmnopqrstuvwxyz",
                "!@#$%^&*_+{}[]|\\:;\"'<>?/~`()_-+="
            };
            _passGenerator = new PassGenerator();
        }

        [Test]
        public void Generate_ShouldReturnPasswordOfCorrectLength()
        {
            // Arrange
            var passLength = 12;

            // Act
            var password = _passGenerator.Generate(passLength, _categories);

            // Assert
            Assert.That(password.Length, Is.EqualTo(passLength));
        }

        [Test]
        public void Generate_ShouldContainCharactersFromAllCategories()
        {
            // Arrange
            var passLength = 12;

            // Act
            var password = _passGenerator.Generate(passLength, _categories);

            // Assert
            foreach (var category in _categories)
            {
                Assert.That(password.Any(category.Contains), Is.True, $"Password does not contain characters from category: {category}");
            }
        }
        
        [Test]
        public void Generate_ShouldThrowException_WhenPassLengthLessThanCategoryLength()
        {
            // Arrange
            var passLength = 3; // Less than the number of categories

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passGenerator.Generate(passLength, _categories));
        }
        
    }
}