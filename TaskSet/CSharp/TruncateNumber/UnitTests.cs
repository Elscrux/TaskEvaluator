using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_3_5() {
        var leftover = TaskClass.TruncateNumber(3.5);
        Assert.Equal(0.5, leftover, 6);
    }
    
    [Fact]
    public void Test_3_14159265359() {
        var leftover = TaskClass.TruncateNumber(3.14159265359);
        Assert.Equal(0.14159265359, leftover, 6);
    }
    
    [Fact]
    public void Test_1_0() {
        var leftover = TaskClass.TruncateNumber(1.0);
        Assert.Equal(0.0, leftover, 6);
    }
    
    [Fact]
    public void Test_0_0() {
        var leftover = TaskClass.TruncateNumber(0.0);
        Assert.Equal(0.0, leftover, 6);
    }
    
    [Fact]
    public void Test_1_33() {
        var leftover = TaskClass.TruncateNumber(1.33);
        Assert.Equal(0.33, leftover, 6);
    }
    
    [Fact]
    public void Test_123_456() {
        var leftover = TaskClass.TruncateNumber(123.456);
        Assert.Equal(0.456, leftover, 6);
    }
}