using Xunit;
namespace Task;

public class Test_DecodeShift {
    [Fact]
    public void Test_0() {
        const string letters = "abcdefghijklmnopqrstuvwxyz";
        foreach (var i in Enumerable.Range(0, 100)) {
            var str = string.Join(string.Empty, Enumerable.Range(0, 10).Select(_ => letters[new Random().Next(letters.Length)]));
            var encodedStr = TaskClass.EncodeShift(str);
            var decodedStr = TaskClass.DecodeShift(encodedStr);
            Assert.Equal(str, decodedStr);
        }
    }
}