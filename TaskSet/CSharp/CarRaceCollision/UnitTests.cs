using Xunit;
namespace Task;

public class Test_CarRaceCollision {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CarRaceCollision(2);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CarRaceCollision(3);
        Assert.Equal(9, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CarRaceCollision(4);
        Assert.Equal(16, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CarRaceCollision(8);
        Assert.Equal(64, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CarRaceCollision(10);
        Assert.Equal(100, result);
    }
}