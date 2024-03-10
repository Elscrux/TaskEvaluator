using Xunit;
namespace Task;

public class Test_FindZero {
    [Fact]
    public void Test_0() {
        var random = new Random(42);
        for (var i = 0; i < 100; i++) {
            var ncoeff = 2 * random.Next(1, 4);
            List<double> coeffs = [];
            for (var j = 0; j < ncoeff; j++) {
                var coeff = random.Next(-10, 10);
                if (coeff == 0) coeff = 1;

                coeffs.Add(coeff);
            }

            var solution = TaskClass.FindZero(coeffs);
            var zero = TaskClass.Poly(coeffs, solution);
            Assert.True(Math.Abs(zero)  < 1e-4, $"Expected 0 but got {zero} for coefficients {string.Join(", ", coeffs)}");
        }
    }
}