using System.Diagnostics;
using ScottPlot;
using task14;

class task15
{
    static void Main()
    {
        var SIN = (double x) => Math.Sin(x);
        double[] steps = new double[]{1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6};
        
        var tests = new Dictionary<double, int>()
        {
            { 1e-1, 0},
            { 1e-2, 0},
            { 1e-3, 0},
            { 1e-4, 0},
            { 1e-5, 0},
            { 1e-6, 0}
        };
        
        TimeSpan min_time = TimeSpan.MaxValue;
        double best_step = 0;

        for (int i = 0; i < 5; i++)
        {
            foreach (double step in steps)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                double result = DefiniteIntegral.Solve_OneThread(-100, 100, SIN, step);

                timer.Stop();

                TimeSpan time = timer.Elapsed;
                if (Math.Abs(result) < 1e-4)
                {

                    if (time < min_time)
                    {
                        min_time = time;
                        best_step = step;
                    }
                }
            }

            tests[best_step] += 1;
        }
        
        int max_threads = 10; 
        int runs = 100; 

        double one_thread_time = 0;
        for (int i = 0; i < runs; i++)
        {
            Stopwatch stop_watch = Stopwatch.StartNew();
            var result = DefiniteIntegral.Solve_OneThread(-100, 100, SIN, best_step);
            stop_watch.Stop();
            one_thread_time += stop_watch.Elapsed.TotalMilliseconds;
        }
        one_thread_time /= runs;

        double[] times = new double[max_threads];
        double[] threads_count = new double[max_threads];

        double best_time = double.MaxValue;
        int best_thread_count = 1;

        for (int threads = 1; threads <= max_threads; threads++)
        {
            double total_time = 0;
            for (int i = 0; i < runs; i++)
            {
                Stopwatch stop_watch = Stopwatch.StartNew();
                var result = DefiniteIntegral.Solve(-100, 100, SIN, best_step, threads);
                stop_watch.Stop();
                total_time += stop_watch.Elapsed.TotalMilliseconds;
            }
            double average_time = total_time / runs;
            times[threads - 1] = average_time;
            threads_count[threads - 1] = threads;


            if (average_time < best_time)
            {
                best_time = average_time;
                best_thread_count = threads;
            }
        }

        var plt = new ScottPlot.Plot();
        plt.Add.Scatter(times.ToArray(), threads_count.Select(t => (double)t).ToArray());
        plt.XLabel("Время вычисления функции Solve (мс)");
        plt.YLabel("Количество потоков");
        plt.Title("Время выполнения от числа потоков");
        string path = @"D:\универ\practise1\SummerPractise2025\task14\plot_no.png";
        plt.SavePng(path, 600, 400);

        string file = @"D:\универ\practise1\SummerPractise2025\task14\review_no.txt";
        using (StreamWriter writer = new StreamWriter(file))
        {
            writer.WriteLine($"Шаг: {best_step}");
            writer.WriteLine($"Однопоточное время: {Math.Round(one_thread_time, 2)} мс");
            writer.WriteLine($"Лучшее многопоточное время: {Math.Round(best_time, 2)} мс при {best_thread_count} потоках");
            double difference = (one_thread_time - best_time) / one_thread_time * 100;
            writer.WriteLine($"Ускорение: {Math.Round(difference, 2)}%");
        }   
    }
}
