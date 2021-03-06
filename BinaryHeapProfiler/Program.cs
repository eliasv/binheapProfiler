﻿//#define DEBUG
#define PROFILE
//#define PROFILE_RAP
//#define PROFILE_DAIC
//#define PROFILE_IHOD
#define PROFILE_ASE
#define PROFILE_ASE_C1
#define PROFILE_ASE_C2

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
            uint[] N = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int maxPowerN = 5;
            uint k, iterator=0;
            int Cols = 8;
            double[,] Table = new double[maxPowerN * (10 - 1), Cols];
            heap<int> H;
#endregion



#region Profiling Code
#if (PROFILE)
            Console.WriteLine("Profiling: Start");
            #region Random Array Profiling
#if (PROFILE_RAP)
            Console.WriteLine("Profiling: Random Array Profiling : Start");
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (uint)(Math.Pow(10, p));
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
            Console.WriteLine("Profiling: Random Array Profiling : End");
#endif

#endregion

#region Heap Profiling: Declaration and Array Initialization copy
#if(PROFILE_DAIC)
            Console.WriteLine("Profiling: Declaration and Array Initialization copy : Start");
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                repeats = 0;
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (uint)(Math.Pow(10, p));
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
                    //Table[iterator, 0] = k;  // Data filled on previous profile
                    Table[iterator++, 2] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }
            Console.WriteLine("Profiling: Declaration and Array Initialization copy : End");
#endif
#endregion

            #region Heap Profiling: Initial heapification of data.
#if(PROFILE_IHOD)
            Console.WriteLine("Profiling: Initial heapification of data : Start");
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                repeats = 0;
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (uint)(Math.Pow(10, p));
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
                    //Table[iterator, 0] = k;  // Data filled on previous profile
                    Table[iterator++, 3] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }
            Console.WriteLine("Profiling: Initial heapification of data : End");
#endif
            #endregion

            #region Heap Profiling: Add single element to heap.
            Console.WriteLine("Profiling: Add single element to heap : Start");
#if(PROFILE_ASE)
            // TODO: create use cases for possible occurences.
            //  case 1: add element with heap array resize.
            //  case 2: add element without heap array resize
#if (PROFILE_ASE_C1)
            Console.WriteLine("Profiling: Add single element to heap : Case 1 : Start");
            // Case 1
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                repeats = 0;
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (uint)(Math.Pow(10, p));
                    A = new RandomArray<int>(k);
                    H = new heap<int>(A.data, k);
                    Random randValue = new Random();
                    H.buildMinHeap();
                    // Profiling Starts
                    repeats = 0;
                    timekeeper.Restart();
                    while (timekeeper.ElapsedMilliseconds < Tmax)
                    {
                        H.insertElement(randValue.Next());
                        repeats++;
                    }
                    timekeeper.Stop();
                    // Profiling ends
                    //Table[iterator, 0] = k;  // Data filled on previous profile
                    Table[iterator++, 4] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }

            Console.WriteLine("Profiling: Add single element to heap : Case 1 : End");
#endif
            //Case 2
#if (PROFILE_ASE_C2)
            Console.WriteLine("Profiling: Add single element to heap : Case 2 : Start");
            iterator = 0;
            for (var p = 0; p < maxPowerN; p++)
            {
                repeats = 0;
                for (var i = 0; i < N.Length; i++)
                {
                    k = N[i] * (uint)(Math.Pow(10, p));
                    A = new RandomArray<int>(k);
                    H = new heap<int>(A.data, 2^31);
                    Random randValue = new Random();
                    H.buildMinHeap();
                    // Profiling Starts
                    repeats = 0;
                    timekeeper.Restart();
                    while (timekeeper.ElapsedMilliseconds < Tmax)
                    {
                        H.insertElement(randValue.Next());
                        repeats++;
                    }
                    timekeeper.Stop();
                    // Profiling ends
                    //Table[iterator, 0] = k;  // Data filled on previous profile
                    Table[iterator++, 5] = (double)timekeeper.ElapsedMilliseconds / ((double)repeats * 1000);
                }

            }
            Console.WriteLine("Profiling: Add single element to heap : Case 2 : End");
#endif
            Console.WriteLine("Profiling: Add single element to heap : End");
#endif
            Console.WriteLine("Profiling: End");

            printTable(Table, maxPowerN * (10 - 1), Cols);
#endregion


#endif

#endregion

            #region Testing
#if (DEBUG)
            int[] A1 = { 9, 7, 8, 1, 4 };
            int[] A2 = { 2, 6, 3, 19, 0 };
            heap<int> H1 = new heap<int>(A1, 5);
            heap<int> H2 = new heap<int>(A2, 5);
            Console.WriteLine("****** H1 ******");
            H1.print();
            H1.buildMinHeap();
            H1.print();
            Console.WriteLine("****** H2 ******");
            H2.print();
            H2.buildMinHeap();
            H2.print();
            H1.union(H2);
            Console.WriteLine("****** (H1)U(H2) ******");
            H1.print();
            Console.WriteLine("****** (H2)U(H1) ******");
            H2.union(A1);
            H2.print();
            
#endif
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
