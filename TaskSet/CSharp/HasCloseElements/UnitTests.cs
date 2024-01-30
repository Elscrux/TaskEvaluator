using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_3_Elements_0_5_Threshold() {
        var any = TaskClass.HasCloseElements([1, 2, 3], 0.5);
        Assert.Equal(true, any);
    }

    [Fact]
    public void Test_3_Elements_0_1_Threshold() {
        var any = TaskClass.HasCloseElements([1, 2, 3], 0.1);
        Assert.Equal(false, any);
    }

    [Fact]
    public void Test_6_Elements_0_3_Threshold() {
        var any = TaskClass.HasCloseElements([1.0, 2.0, 3.9, 4.0, 5.0, 2.2], 0.3);
        Assert.Equal(true, any);
    }

    [Fact]
    public void Test_6_Elements_0_05_Threshold() {
        var any = TaskClass.HasCloseElements([1.0, 2.0, 3.9, 4.0, 5.0, 2.2], 0.05);
        Assert.Equal(false, any);
    }
    
    [Fact]
    public void Test_5_Elements_0_95_Threshold() {
        var any = TaskClass.HasCloseElements([1.0, 2.0, 5.9, 4.0, 5.0], 0.95);
        Assert.Equal(true, any);
    }
    
    [Fact]
    public void Test_5_Elements_0_8_Threshold() {
        var any = TaskClass.HasCloseElements([1.0, 2.0, 5.9, 4.0, 5.0], 0.8);
        Assert.Equal(false, any);
    }
    
    [Fact]
    public void Test_6_Elements_0_1_Threshold() {
        var any = TaskClass.HasCloseElements([1.0, 2.0, 3.0, 4.0, 5.0, 2.0], 0.1);
        Assert.Equal(true, any);
    }
    
    [Fact]
    public void Test_5_Elements_1_0_Threshold() {
        var any = TaskClass.HasCloseElements([1.1, 2.2, 3.1, 4.1, 5.1], 1.0);
        Assert.Equal(true, any);
    }
    
    [Fact]
    public void Test_5_Elements_0_5_Threshold() {
        var any = TaskClass.HasCloseElements([1.1, 2.2, 3.1, 4.1, 5.1], 0.5);
        Assert.Equal(false, any);
    }
}