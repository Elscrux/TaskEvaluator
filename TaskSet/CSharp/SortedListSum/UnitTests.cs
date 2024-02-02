using Xunit;
namespace Task;

public class Test_SortedListSum {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SortedListSum(["aa", "a", "aaa"]);
        Assert.Equal(["aa"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SortedListSum(["school", "AI", "asdf", "b"]);
        Assert.Equal(["AI", "asdf", "school"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SortedListSum(["d", "b", "c", "a"]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SortedListSum(["d", "dcba", "abcd", "a"]);
        Assert.Equal(["abcd", "dcba"], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SortedListSum(["AI", "ai", "au"]);
        Assert.Equal(["AI", "ai", "au"], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SortedListSum(["a", "b", "b", "c", "c", "a"]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SortedListSum(["aaaa", "bbbb", "dd", "cc"]);
        Assert.Equal(["cc", "dd", "aaaa", "bbbb"], result);
    }
}