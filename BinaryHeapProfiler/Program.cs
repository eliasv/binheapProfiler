using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BinaryHeapProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
#region Object and variable Declarations
            Stopwatch timekeeper = new Stopwatch();
            RandomArray<int> A;
            int repeats = 0;
            long Tmax = 2000;
            ulong[] N = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int maxPowerN = 3;
            ulong k, iterator=0;
            int Cols = 4;
            double[,] Table = new double[maxPowerN * (10 - 1), Cols];
            heap<int> H;
#endregion

#region Random Array Profiling 
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (ulong)(Math.Pow(10, p));
                    // Profiling Starts
                    repeats = 0;
                    timekeeper.Restart();
                    while (timekeeper.ElapsedMilliseconds < Tmax)
                    {
                        A = new RandomArray<int>(k);
                        repeats++;
                    }
                    timekeeper.Stop();
                    // Profiling ends
                    Table[iterator, 0] = k;
                    Table[iterator++, 1] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }

#endregion


#region Heap Profiling: Declaration and Array Initialization copy
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                repeats = 0;
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (ulong)(Math.Pow(10, p));
                    A = new RandomArray<int>(k);  
                    // Profiling Starts
                    repeats = 0;
                    timekeeper.Restart();
                    while (timekeeper.ElapsedMilliseconds < Tmax)
                    {
                        H = new heap<int>(A.data, k);
                        repeats++;
                    }
                    timekeeper.Stop();
                    // Profiling ends
                    Table[iterator, 0] = k;  // Data filled on previous profile
                    Table[iterator++, 2] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }
            printTable(Table, maxPowerN * (10 - 1), Cols);
 
#endregion

            #region Heap Profiling: Initial heapification of data.

            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                repeats = 0;
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (ulong)(Math.Pow(10, p));
                    A = new RandomArray<int>(k);
                    H = new heap<int>(A.data, k);
                    // Profiling Starts
                    repeats = 0;
                    timekeeper.Restart();
                    while (timekeeper.ElapsedMilliseconds < Tmax)
                    {
                        H.buildMinHeap();
                        repeats++;
                    }
                    timekeeper.Stop();
                    // Profiling ends
                    Table[iterator, 0] = k;  // Data filled on previous profile
                    Table[iterator++, 3] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }
            printTable(Table, maxPowerN * (10 - 1), Cols);
            
            #endregion

            Console.ReadLine();
        }

        public static void printTable(double[,] A, int MaxColumn1, int MaxColumn2)
        {
            Console.WriteLine("RunTime:");
            for (var i = 0; i < MaxColumn1-1; i++)
            {
                for (var j = 0; j < MaxColumn2; j++)
                {
                    Console.Write("| {0,-10:0.000E+0}", A[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void printElapsedTime(TimeSpan ts)
        {
            /// Source
            ///     MSDN stopwatch Class documentation.
            ///         http://msdn.microsoft.com/en-us/library/system.diagnostics.stopwatch(v=vs.110).aspx
            ///     Accessed: 11/8/2014

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }


}
