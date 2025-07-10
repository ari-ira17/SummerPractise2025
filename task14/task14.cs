using System.Threading;

namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double square = 0;
        double length_for_thread = (b - a) / threadsnumber;

        Thread[] threads = new Thread[threadsnumber];
        Barrier barrier = new Barrier(threadsnumber);
        object locker = new object();

        for (int i = 0; i < threadsnumber; i++)
        {
            int i_therad = i;

            threads[i] = new Thread(() => 
            {
                double x1 = a + i_therad * length_for_thread;
                double x2;

                if (i + 1 != threadsnumber)
                { 
                    x2 = x1 + length_for_thread; 
                }
                else { x2 = b; }

                double s_thread = 0;
                
                for (double j = x1; j < x2; j += step)
                {
                    double x1_thread = j;
                    double x2_thread = Math.Min(j + step, x2);
                    s_thread += 0.5 * (function(x1_thread) + function(x2_thread)) * (x2_thread - x1_thread);
                }

                lock (locker)
                { square += s_thread;}

                barrier.SignalAndWait();
            });
        }

        foreach (var thread in threads)
        { thread.Start(); }

        foreach (var thread in threads)
        { thread.Join(); }

        return square;
    }
}
