namespace Task;

public static class TaskClass {
    /// <summary>
    ///  xs are coefficients of a polynomial. find_zero find x such that poly(x) = 0. find_zero returns only only zero point, even if there are many. Moreover, find_zero only takes list xs having even number of coefficients and largest non zero coefficient as it guarantees a solution. 
    /// These helper functions are available: double Poly(list @xs, double @x)
    /// </summary>
    public static double FindZero(List<double> @xs) {
        //INSERT_CODE_HERE
    }

    /// <summary>
    /// Evaluates polynomial with coefficients xs at point x.
    /// </summary>
    /// <returns>xs[0] + xs[1] * x + xs[1] * x^2 + .... xs[n] * x^n</returns>
    public static double Poly(List<double> @xs, double @x) {
        return xs.Select((coeff, i) => coeff * Math.Pow(x, i)).Sum();
    }
}