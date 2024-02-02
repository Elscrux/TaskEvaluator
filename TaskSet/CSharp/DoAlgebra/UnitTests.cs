using Xunit;
namespace Task;

public class Test_DoAlgebra {
    [Fact]
    public void Test_0() {
        var result = TaskClass.DoAlgebra(["**", "*", "+"], [2, 3, 4, 5]);
        Assert.Equal(37, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.DoAlgebra(["+", "*", "-"], [2, 3, 4, 5]);
        Assert.Equal(9, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.DoAlgebra(["//", "*"], [7, 3, 4]);
        Assert.Equal(8, result);
    }
}