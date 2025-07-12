using System.Threading;

namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double length_for_thread = (b - a) / threadsnumber;
        double[] partial_sums = new double[threadsnumber];

        Parallel.For(0, threadsnumber, i =>
        {
            double x1 = a + i * length_for_thread;
            double x2 = (i == threadsnumber - 1) ? b : x1 + length_for_thread;

            double local_sum = 0;
            for (double x = x1; x < x2; x += step)
            {
                double x_next = Math.Min(x + step, x2);
                local_sum += 0.5 * (function(x) + function(x_next)) * (x_next - x);
            }
            partial_sums[i] = local_sum;
        });

        return partial_sums.Sum();
    }

    public static double Solve_OneThread(double a, double b, Func<double, double> function, double step)        
    {
        double square = 0;
        int n = (int)( (b - a) / step);

        for (int i = 0; i < n; i++) 
        {
            double x1 = a + i * step;
            double x2 = a + (i + 1) * step;
            double s = 0.5 * (function(x1) + function(x2)) * step;

            square += s;
        }   

        return square;
    } 
}
