using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Model;

namespace TestsForPasswordForUs;

public class SynchronizerTests
{
    [Test]
    public void MergeData_ShouldReturnDefaultData_WhenBothDataArrayAreEmpty()
    {
        // Arrange
        var data1 = new SynchronizeData(Guid.Empty, 0,new List<NodeDataModel>());
        var data2 = new SynchronizeData(Guid.Empty, 0,new List<NodeDataModel>());
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.EqualTo(Guid.Empty));
            Assert.That(result.Data, Is.Empty);
        });
    }

    [Test]
    public void MergeData_ShouldNotMergeDataArray_WhenBothDataArrayAreEquals()
    {
        // Arrange
        var version = Guid.NewGuid();
        var guid = Guid.NewGuid();
        var time = DateTime.Now;
        var data1 = new SynchronizeData(version, time.Ticks,new List<NodeDataModel>
        {
            new NodeDataModel {Guid = guid, Id = 1, ChangeTimeTicks = time.Ticks, Title = "Node1" }
        });
        var data2 = new SynchronizeData(version, time.Ticks, new List<NodeDataModel>
        {
            new NodeDataModel {Guid = guid, Id = 1, ChangeTimeTicks = time.Ticks, Title = "Node1" }
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.EqualTo(version));
            Assert.That(result.Data, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public void MergeData_ShouldMergeTwoDataArray_WhenBothDataArrayAreAreDifferent()
    {
        // Arrange
        var time = DateTime.Now;
        var time2 = DateTime.Now.AddDays(1);
        var data1 = new SynchronizeData(Guid.NewGuid(), time.Ticks,new List<NodeDataModel>
        {
            new NodeDataModel {Guid = Guid.NewGuid(), Id = 1, ChangeTimeTicks = time.Ticks, Title = "Node1" }
        });
        var data2 = new SynchronizeData(Guid.NewGuid(), time2.Ticks,new List<NodeDataModel>
        {
            new NodeDataModel {Guid = Guid.NewGuid(), Id = 2, ChangeTimeTicks = time2.Ticks, Title = "Node2" }
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.Not.EqualTo(data1.Version));
            Assert.That(result.Version, Is.Not.EqualTo(data2.Version));
            Assert.That(result.ChangeTimeTicks, Is.Not.EqualTo(time.Ticks));
            Assert.That(result.ChangeTimeTicks, Is.Not.EqualTo(time2.Ticks));
            Assert.That(result.Data, Has.Count.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
            Assert.That(result.Data[1].Id, Is.EqualTo(2));
            Assert.That(result.Data[1].Title, Is.EqualTo("Node2"));
        });
    }

    [Test]
    public void MergeData_ShouldReturnFirstDataArray_WhenFirstArrayContainSecond()
    {
        // Arrange
        var version1 = Guid.NewGuid();
        var version2 = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var time = DateTime.Now;
        var time2 = DateTime.Now.AddDays(-1);
        var data1 = new SynchronizeData(version1, time.Ticks, new List<NodeDataModel>
        {
            new NodeDataModel { Guid = guid1, Id = 1, ChangeTimeTicks = time2.Ticks, Title = "Node1" },
            new NodeDataModel {Guid = Guid.NewGuid(), Id = 2, ChangeTimeTicks = time.Ticks, Title = "Node2" }
        });
        var data2 = new SynchronizeData(version2, time2.Ticks, new List<NodeDataModel>
        {
            new NodeDataModel { Guid = guid1, Id = 1, ChangeTimeTicks = time2.Ticks, Title = "Node1" }
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.EqualTo(version1));
            Assert.That(result.Data, Has.Count.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid1));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
            Assert.That(result.Data[1].Id, Is.EqualTo(2));
            Assert.That(result.Data[1].Title, Is.EqualTo("Node2"));
        });
    }

    [Test]
    public void MergeData_ShouldReturnSecondDataArray_WhenSecondArrayContainFirst()
    {
        // Arrange
        var version1 = Guid.NewGuid();
        var version2 = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var time = DateTime.Now.AddDays(-1);
        var time2 = DateTime.Now;
        var data1 = new SynchronizeData(version1,  time.Ticks,new List<NodeDataModel>
        {
            new NodeDataModel { Guid = guid1, Id = 1, ChangeTimeTicks = time.Ticks,Title = "Node1" }
        });
        var data2 = new SynchronizeData(version2,  time2.Ticks,new List<NodeDataModel>
        {
            new NodeDataModel { Guid = guid1, Id = 1,  ChangeTimeTicks = time.Ticks, Title = "Node1" },
            new NodeDataModel {Guid = Guid.NewGuid(), Id = 2,  ChangeTimeTicks = time2.Ticks, Title = "Node2" }
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.EqualTo(version2));
            Assert.That(result.Data, Has.Count.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid1));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
            Assert.That(result.Data[1].Id, Is.EqualTo(2));
            Assert.That(result.Data[1].Title, Is.EqualTo("Node2"));
        });
    }

    [Test]
    public void MergeData_ShouldReturnFirstDataArray_WhenFirstHasChangedField()
    {
        // Arrange
        var version1 = Guid.NewGuid();
        var version2 = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var time = DateTime.Now;
        var oldTime = time.AddDays(-1);
        var data1 = new SynchronizeData(version1, time.Ticks, new List<NodeDataModel>
        {
            new NodeDataModel {Guid = guid1, ChangeTimeTicks = time.Ticks, Id = 1, Title = "Node1", Login = "newLogin"}
        });
        var data2 = new SynchronizeData(version2,  oldTime.Ticks,new List<NodeDataModel>
        {
            new NodeDataModel {Guid = guid1, ChangeTimeTicks = oldTime.Ticks, Id = 1, Title = "Node1", Login = "oldLogin" }
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.EqualTo(version1));
            Assert.That(result.Data, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid1));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].ChangeTimeTicks, Is.EqualTo(time.Ticks));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
            Assert.That(result.Data[0].Login, Is.EqualTo("newLogin"));
            Assert.That(result.Data[0].Data["Login_"+oldTime.ToShortDateString()+"_"+oldTime.Ticks],Is.EqualTo("oldLogin"));
        });
    }

    [Test]
    public void MergeData_ShouldReturnSecondDataArray_WhenSecondHasChangedField()
    {
        // Arrange
        var version1 = Guid.NewGuid();
        var version2 = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var time = DateTime.Now;
        var oldTime = time.AddDays(-1);
        var data1 = new SynchronizeData(version1,  oldTime.Ticks,new List<NodeDataModel>
        {
            new() { Guid = guid1, ChangeTimeTicks = oldTime.Ticks, Id = 1, Title = "Node1", Login = "oldLogin"}
        });
        var data2 = new SynchronizeData(version2, time.Ticks,new List<NodeDataModel>
        {
            new() { Guid = guid1, ChangeTimeTicks = time.Ticks, Id = 1, Title = "Node1", Login = "newLogin"}
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.EqualTo(version2));
            Assert.That(result.Data, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid1));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].ChangeTimeTicks, Is.EqualTo(time.Ticks));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
            Assert.That(result.Data[0].Login, Is.EqualTo("newLogin"));
            Assert.That(result.Data[0].Data["Login_"+oldTime.ToShortDateString()+"_"+oldTime.Ticks],Is.EqualTo("oldLogin"));
        });
    }

    [Test]
    public void MergeData_ShouldMergeTwoDataArrayWithConflict_WhenBothHasChangedTheSameField()
    {
        // Arrange
        var version1 = Guid.NewGuid();
        var version2 = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var time = DateTime.Now;
        var data1 = new SynchronizeData(version1, time.Ticks,new List<NodeDataModel>
        {
            new() {Guid = guid1, ChangeTimeTicks = time.Ticks, Id = 1, Title = "Node1", Login = "newLogin1"}
        });
        var data2 = new SynchronizeData(version2, time.Ticks,new List<NodeDataModel>
        {
            new() {Guid = guid1, ChangeTimeTicks = time.Ticks, Id = 1, Title = "Node1", Login = "newLogin2"}
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.Not.EqualTo(version1));
            Assert.That(result.Version, Is.Not.EqualTo(version2));
            Assert.That(result.ChangeTimeTicks, Is.Not.EqualTo(time.Ticks));
            Assert.That(result.Data, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid1));
            Assert.That(result.Data[0].ChangeTimeTicks, Is.EqualTo(time.Ticks));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            Assert.That(result.Data[0].Title, Is.EqualTo("Node1"));
            Assert.That(result.Data[0].Login, Is.EqualTo("newLogin1"));
            Assert.That(result.Data[0].Data["Login_"+time.ToShortDateString()+"_"+time.Ticks],Is.EqualTo("newLogin2"));
        });
    }

    [Test]
    public void MergeData_ShouldMergeTwoDataArray_WhenBothHasChangedField()
    {
        // Arrange
        var version1 = Guid.NewGuid();
        var version2 = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var time = DateTime.Now;
        var data1 = new SynchronizeData(version1, time.Ticks,new List<NodeDataModel>
        {
            new() { Guid = guid1, ChangeTimeTicks = time.Ticks, Id = 1, Title = "newName", Login = "Login"}
        });
        var data2 = new SynchronizeData(version2, time.Ticks,new List<NodeDataModel>
        {
            new() { Guid = guid1,  ChangeTimeTicks = time.Ticks, Id = 1, Title = "Name", Login = "newLogin"}
        });
        var synchronizer = new TestSynchronizer();

        // Act
        var result = synchronizer.MergeData(data1, data2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Version, Is.Not.EqualTo(version1));
            Assert.That(result.Version, Is.Not.EqualTo(version2));
            Assert.That(result.Data, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Data[0].Guid, Is.EqualTo(guid1));
            Assert.That(result.Data[0].ChangeTimeTicks, Is.EqualTo(time.Ticks));
            Assert.That(result.Data[0].Id, Is.EqualTo(1));
            
            Assert.That(result.Data[0].Title, Is.EqualTo("newName"));
            Assert.That(result.Data[0].Login, Is.EqualTo("Login"));
            Assert.That(result.Data[0].Data["Title_"+time.ToShortDateString()+"_"+time.Ticks],Is.EqualTo("Name"));
            Assert.That(result.Data[0].Data["Login_"+time.ToShortDateString()+"_"+time.Ticks],Is.EqualTo("newLogin"));
        });
    }
}

public class TestSynchronizer() : Synchronizer(null!)
{
    public override void SynchronizeStorage(SynchronizationDataModel data)
    {
        throw new System.NotImplementedException();
    }
}