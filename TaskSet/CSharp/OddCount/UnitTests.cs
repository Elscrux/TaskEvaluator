using Xunit;
namespace Task;

public class Test_OddCount {
    [Fact]
    public void Test_0() {
        var result = TaskClass.OddCount(["1234567"]);
        Assert.Equal(["the number of odd elements 4n the str4ng 4 of the 4nput."], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.OddCount(["3","11111111"]);
        Assert.Equal(["the number of odd elements 1n the str1ng 1 of the 1nput.", "the number of odd elements 8n the str8ng 8 of the 8nput."], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.OddCount(["271", "137", "314"]);
        Assert.Equal([
        "the number of odd elements 2n the str2ng 2 of the 2nput.",
        "the number of odd elements 3n the str3ng 3 of the 3nput.",
        "the number of odd elements 2n the str2ng 2 of the 2nput."
    ], result);
    }
}