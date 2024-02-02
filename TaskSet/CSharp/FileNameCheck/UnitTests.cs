using Xunit;
namespace Task;

public class Test_FileNameCheck {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FileNameCheck("example.txt");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FileNameCheck("1example.dll");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FileNameCheck("s1sdf3.asd");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FileNameCheck("K.dll");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.FileNameCheck("MY16FILE3.exe");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.FileNameCheck("His12FILE94.exe");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.FileNameCheck("_Y.txt");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.FileNameCheck("?aREYA.exe");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.FileNameCheck("/this_is_valid.dll");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.FileNameCheck("this_is_valid.wow");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.FileNameCheck("this_is_valid.txt");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.FileNameCheck("this_is_valid.txtexe");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.FileNameCheck("#this2_i4s_5valid.ten");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_13() {
        var result = TaskClass.FileNameCheck("@this1_is6_valid.exe");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_14() {
        var result = TaskClass.FileNameCheck("this_is_12valid.6exe4.txt");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_15() {
        var result = TaskClass.FileNameCheck("all.exe.txt");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_16() {
        var result = TaskClass.FileNameCheck("I563_No.exe");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_17() {
        var result = TaskClass.FileNameCheck("Is3youfault.txt");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_18() {
        var result = TaskClass.FileNameCheck("no_one#knows.dll");
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_19() {
        var result = TaskClass.FileNameCheck("1I563_Yes3.exe");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_20() {
        var result = TaskClass.FileNameCheck("I563_Yes3.txtt");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_21() {
        var result = TaskClass.FileNameCheck("final..txt");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_22() {
        var result = TaskClass.FileNameCheck("final132");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_23() {
        var result = TaskClass.FileNameCheck("_f4indsartal132.");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_24() {
        var result = TaskClass.FileNameCheck(".txt");
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_25() {
        var result = TaskClass.FileNameCheck("s.");
        Assert.Equal("No", result);
    }
}