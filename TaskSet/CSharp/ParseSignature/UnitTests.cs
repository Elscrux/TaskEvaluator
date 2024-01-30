using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_has_close_elements() {
        var signature = TaskClass.Parse("def has_close_elements(numbers: List[float], threshold: float) -> bool:\n");
        Assert.Equal(
            new FunctionSignature(
                "bool",
                "has_close_elements",
                [
                    new Parameter("List[float]", "numbers"),
                    new Parameter("float", "threshold")
                ]),
            signature);
    }
}