using Xunit;
namespace Task;

public class Test_CheckDictCase {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CheckDictCase({"p":"pineapple", "b":"banana"});
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CheckDictCase({"p":"pineapple", "A":"banana", "B":"banana"});
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CheckDictCase({"p":"pineapple", 5:"banana", "a":"apple"});
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CheckDictCase({"Name":"John", "Age":"36", "City":"Houston"});
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CheckDictCase({"STATE":"NC", "ZIP":"12345" });
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CheckDictCase({"fruit":"Orange", "taste":"Sweet" });
        Assert.Equal(true+, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CheckDictCase(new Dictionary<object, object>());
        Assert.Equal(false, result);
    }
}