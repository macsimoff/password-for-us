using PasswordForUs.Command.Builder.DataBuilder;
using PasswordForUs.Const;
using PasswordForUs.Model;

namespace TestsForPasswordForUs.DataBuilderTest;

public class FindCommandDataBuilderTest
{
    private FindCommandDataBuilder _builder;

    [SetUp]
    public void SetUp()
    {
        _builder = new FindCommandDataBuilder();
    }

    [Test]
    public void Build_WithSimpleText_ShouldReturnDataWithUrlAndNameValue()
    {
        // Arrange
        var commandData = new[] { "google.com" };
        var expected = new FindCommandData("google.com");

        // Act
        var actual = _builder.Build(commandData);

        // Assert
        Assert.AreEqual(expected.UrlText, actual.UrlText);
    }

    [Test]
    public void Build_WithSpecifyIdKey_ShouldReturnDataWithIdValue()
    {
        // Arrange
        var commandData = new[]
        {
            FindKeyConst.IdKey, "1"
        };
        var expected = new FindCommandData
        {
            Id = 1
        };

        // Act
        var actual = _builder.Build(commandData);

        // Assert
        Assert.AreEqual(expected.Id, actual.Id);
    }

    [Test]
    public void Build_WithSpecifyUrlKey_ShouldReturnDataWithUrlValue()
    {
        // Arrange
        var commandData = new[]
        {
            FindKeyConst.UrlKey, "http://example.com"
        };
        var expected = new FindCommandData
        {
            UrlText = "http://example.com"
        };

        // Act
        var actual = _builder.Build(commandData);

        // Assert
        Assert.AreEqual(expected.UrlText, actual.UrlText);
    }

    [Test]
    public void Build_WithSpecifyNameKey_ShouldReturnDataWithNameValue()
    {
        // Arrange
        var commandData = new[]
        {
            FindKeyConst.NameKey, "exampleName"
        };
        var expected = new FindCommandData
        {
            NameText = "exampleName"
        };

        // Act
        var actual = _builder.Build(commandData);

        // Assert
        Assert.AreEqual(expected.NameText, actual.NameText);
    }
}