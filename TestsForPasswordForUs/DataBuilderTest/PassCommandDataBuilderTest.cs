using PasswordForUs.Command.Builder.DataBuilder;
using PasswordForUs.Model;

namespace TestsForPasswordForUs.DataBuilderTest;

public class PassCommandDataBuilderTest
{
    [Test]
    public void CreateCommandData_ShouldReturnExpectedPassCommandData()
    {
        // Arrange
        var commandData = new[] { "--user", "user", "--url", "url", "--name", "name", "--pass", "pass", "--login", "login", "--key1", "value1", "--key2", "value2" };
        var builder = new PassCommandDataBuilder();
        var expected = new PassCommandData()
        {
            User = "user",
            Url = "url",
            Title = "name",
            Pass = "pass",
            Login = "login",
            Data = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" }
            }
        };

        // Act
        var actual = builder.CreateCommandData(commandData);

        // Assert
        Assert.That(expected.User,  Is.EqualTo(actual.User));
        Assert.That(expected.Url,   Is.EqualTo(actual.Url));
        Assert.That(expected.Title,  Is.EqualTo(actual.Title));
        Assert.That(expected.Pass,  Is.EqualTo(actual.Pass));
        Assert.That(expected.Login, Is.EqualTo(actual.Login));
        Assert.That(expected.Data["key1"],  Is.EqualTo(actual.Data?["key1"]));
        Assert.That(expected.Data["key2"],  Is.EqualTo(actual.Data?["key2"]));
    }
}