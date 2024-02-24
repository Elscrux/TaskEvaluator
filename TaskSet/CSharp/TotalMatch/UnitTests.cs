using Xunit;
namespace Task;

public class Test_TotalMatch {
    [Fact]
    public void Test_0() {
        var result = TaskClass.TotalMatch(["hi", "admin"], ["hi", "hi"]);
        Assert.Equal(["hi", "hi"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.TotalMatch(["hi", "admin"], ["hi", "hi", "admin", "project"]);
        Assert.Equal(["hi", "admin"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.TotalMatch(["4"], ["1", "2", "3", "4", "5"]);
        Assert.Equal(["4"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.TotalMatch(["hi", "admin"], ["hI", "Hi"]);
        Assert.Equal(["hI", "Hi"], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.TotalMatch(["hi", "admin"], ["hI", "hi", "hi"]);
        Assert.Equal(["hI", "hi", "hi"], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.TotalMatch(["hi", "admin"], ["hI", "hi", "hii"]);
        Assert.Equal(["hi", "admin"], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.TotalMatch([], ["this"]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.TotalMatch(["this"], []);
        Assert.Equal([], result);
    }
}