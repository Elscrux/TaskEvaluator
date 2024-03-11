using Xunit;
namespace Task;

public class Test_CheckDictCase {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string> {
			{ "p", "pineapple" },
			{ "b", "banana" }
		});
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string> {
			{ "p", "pineapple" },
			{ "A", "banana" },
			{ "B", "banana" }
		});
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string> {
			{ "p", "pineapple" },
			{ "5", "banana" },
			{ "a", "apple" }
		});
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string> {
			{ "Name", "John" },
			{ "Age", "36" },
			{ "City", "Houston" }
		});
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string> {
			{ "STATE", "NC" },
			{ "ZIP", "12345" }
		});
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string> {
			{ "fruit", "Orange" },
			{ "taste", "Sweet" }
		});
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CheckDictCase(new Dictionary<string, string>());
        Assert.Equal(false, result);
    }
}