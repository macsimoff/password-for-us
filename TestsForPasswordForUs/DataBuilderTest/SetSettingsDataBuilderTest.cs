using PasswordForUs.Command.Builder.DataBuilder;
using PasswordForUs.Const;
using PasswordForUs.Model;

namespace TestsForPasswordForUs.DataBuilderTest;

public class SetSettingsDataBuilderTest
{
    [Test]
    public void CreateCommandData_ShouldReturnExpectedSettings()
    {
        // Arrange
        var builder = new SetSettingsDataBuilder();
        var commandData = new[] { 
            SetSettingsKeyConst.PassLengthKey, "10", 
            SetSettingsKeyConst.SyncPathKey, "path1", 
            SetSettingsKeyConst.ImportPathKey, "path2", 
            SetSettingsKeyConst.AutoImportKey, "y", 
            SetSettingsKeyConst.CharSetsKey, "a","b","c", 
            SetShowSettingsKeyConst.AllKey, "y",
            SetShowSettingsKeyConst.IdKey, "y", 
            SetShowSettingsKeyConst.UrlKey, "y", 
            SetShowSettingsKeyConst.UserKey, "y", 
            SetShowSettingsKeyConst.NameKey, "y", 
            SetShowSettingsKeyConst.LoginKey, "y", 
            SetShowSettingsKeyConst.PassKey, "y",
            SetShowSettingsKeyConst.AllDataKey, "y" 
        };
        var expected = new SetSettingsCommandData()
        {
            DefaultPassLength = 10,
            CharacterSets = new[] { "a", "b", "c" },
            SyncDataPath = "path1",
            DefaultImportPath = "path2",
            AutoOpenStorage = true,
            Show = new SetShowSettingsData()
            {
                All = true,
                Id = true,
                Url = true,
                User = true,
                Name = true,
                Login = true,
                Password = true,
                AllData = true
            }
        };
        
        // Act
        var command = builder.CreateCommandData(commandData);

        // Assert
        Assert.That(command, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(command.DefaultPassLength, Is.EqualTo(expected.DefaultPassLength), "DefaultPassLength");
            Assert.That(command.CharacterSets, Is.EqualTo(expected.CharacterSets), "CharacterSets");
            Assert.That(command.SyncDataPath, Is.EqualTo(expected.SyncDataPath), "SyncDataPath");
            Assert.That(command.DefaultImportPath, Is.EqualTo(expected.DefaultImportPath), "DefaultImportPath");
            Assert.That(command.AutoOpenStorage, Is.EqualTo(expected.AutoOpenStorage), "AutoImport");
            Assert.That(command.Show.All, Is.EqualTo(expected.Show.All), "Show.All");
            Assert.That(command.Show.Id, Is.EqualTo(expected.Show.Id), "Show.Id");
            Assert.That(command.Show.Url, Is.EqualTo(expected.Show.Url), "Show.Url");
            Assert.That(command.Show.User, Is.EqualTo(expected.Show.User), "Show.User");
            Assert.That(command.Show.Name, Is.EqualTo(expected.Show.Name), "Show.Name");
            Assert.That(command.Show.Login, Is.EqualTo(expected.Show.Login), "Show.Login");
            Assert.That(command.Show.Password, Is.EqualTo(expected.Show.Password), "Show.Password");
            Assert.That(command.Show.AllData, Is.EqualTo(expected.Show.AllData), "Show.Data");
        });
    }
    
    [Test]
    public void CreateCommandData_ShouldThrowArgumentException_WhenInvalidKey()
    {
        // Arrange
        var builder = new SetSettingsDataBuilder();
        var commandData = new[] { "--invalid-key", "value" };
        
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => builder.CreateCommandData(commandData));
    }
    
    [Test]
    public void CreateCommandData_ShouldReturnExpectedDataNames_WithDataNamesKey()
    {
        // Arrange
        var builder = new SetSettingsDataBuilder();
        var commandData = new[] { "--data-name1", "y", "--data-name2", "n", "--data-name3", "y" };
        var expected = new SetSettingsCommandData()
        {
            Show = new SetShowSettingsData()
            {
                DataNames = new KeyValuePair<string, bool>[]
                {
                    new("data-name1", true),
                    new("data-name2", false),
                    new("data-name3", true)
                }
            }
        };
        
        // Act
        var command = builder.CreateCommandData(commandData);

        // Assert
        Assert.That(command, Is.Not.Null);
        Assert.That(command.Show.DataNames, Is.EqualTo(expected.Show.DataNames));
    }
}