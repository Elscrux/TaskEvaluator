using Xunit;
namespace Task;

public class Test_ChangeBase {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ChangeBase(8, 3);
        Assert.Equal("22", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ChangeBase(9, 3);
        Assert.Equal("100", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ChangeBase(234, 2);
        Assert.Equal("11101010", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ChangeBase(16, 2);
        Assert.Equal("10000", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ChangeBase(8, 2);
        Assert.Equal("1000", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.ChangeBase(7, 2);
        Assert.Equal("111", result);
    }

    [Fact]
    public void Test_6() {
		foreach(var i in Enumerable.Range(2, 8)) {
			var result = TaskClass.ChangeBase(i, i + 1);
			Assert.Equal(i.ToString(), result);
		}
    }
}