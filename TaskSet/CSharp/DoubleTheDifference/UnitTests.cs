using Xunit;
namespace Task;

public class Test_DoubleTheDifference {
    [Fact]
    public void Test_0() {
        var result = TaskClass.DoubleTheDifference([]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.DoubleTheDifference([5, 4]);
        Assert.Equal(25, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.DoubleTheDifference([0.1, 0.2, 0.3]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.DoubleTheDifference([-10, -20, -30]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.DoubleTheDifference([-1, -2, 8]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.DoubleTheDifference([0.2, 3, 5]);
        Assert.Equal(34, result);
    }

    [Fact]
    public void Test_6() {
        // lst = list(range(-99, 100, 2))
        var list = new List<double>();
        for (var i = -99; i < 100; i += 2) {
            list.Add(i);
        }

        // odd_sum = sum([i**2 for i in lst if i%2!=0 and i > 0])
        var oddSum = list
            .Where(i => i % 2 != 0 && i > 0)
            .Select(i => i * i)
            .Sum();

        var result = TaskClass.DoubleTheDifference(list);
        Assert.Equal(oddSum, result);
    }
}