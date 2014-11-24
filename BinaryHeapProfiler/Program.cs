//#define DEBUG
#define PROFILE
//#define PROFILE_RAP
//#define PROFILE_DAIC
//#define PROFILE_IHOD
//#define PROFILE_ASE
//#define PROFILE_ASE_C1
//#define PROFILE_ASE_C2
#define PROFILE_UNION
#define PROFILE_UNION_C1
#define PROFILE_UNION_C1a
//#define PROFILE_UNION_C1b
//#define PROFILE_UNION_C1c
//#define PROFILE_UNION_C2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace BinaryHeapProfiler
{
    class Program
    {
        List<String> ColumnHeaders = new List<string>();
        List<uint> RowHeaders = new List<uint>();
        List<List<double>> cellData = new List<List<double>>();
        static void Main(string[] args)
        {
#region Object and variable Declarations
            Stopwatch timekeeper = new Stopwatch();
            RandomArray<int> A, B;
            int repeats = 0;
            long Tmax = 2000;
            uint[] N = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int maxPowerN = 2;
            uint iterator=0;
            int Cols = 7;
            double[,] Table = new double[maxPowerN * (10 - 1), Cols];
            heap<int> H;
            heap<int> H1 = new heap<int>();
            heap<int> H2 = new heap<int>();
            List<String> ColumnHeaders = new List<string>();
            List<uint> RowHeaders = new List<uint>();
            List<List<double>> cellData = new List<List<double>>();

            for (var p = 0; p <= maxPowerN; p++)
            {
                for (var i = 0; i < N.Length; i++)
                {
                    RowHeaders.Add(N[i] * (uint)(Math.Pow(10, p)));
                    cellData.Add(new List<double>());
                }
            }
#endregion



#region Profiling Code
#if (PROFILE)
            Console.WriteLine("Profiling: Start");
            #region Random Array Profiling
#if (PROFILE_RAP)
            Console.WriteLine("Profiling: Random Array Profiling : Start");
            ColumnHeaders.Add("Profiling: Random Array Profiling");
            iterator = 0;
            foreach(var k in RowHeaders)
            {
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    A = new RandomArray<int>(k);
                    repeats++;
                }
                timekeeper.Stop();
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds 
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profiling: Random Array Profiling : End");
#endif

#endregion

            #region Heap Profiling: Declaration and Array Initialization copy
#if(PROFILE_DAIC)
            Console.WriteLine("Profiling: Declaration and Array Initialization copy : Start");
            ColumnHeaders.Add("Profiling: Declaration and Array Initialization copy");
            iterator = 0;
            foreach (var k in RowHeaders)
            {
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
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds 
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profiling: Declaration and Array Initialization copy : End");
#endif
#endregion

            #region Heap Profiling: Initial heapification of data.
#if(PROFILE_IHOD)
            Console.WriteLine("Profiling: Initial heapification of data : Start");
            ColumnHeaders.Add("Profiling: Initial heapification of data");
            iterator = 0;
            foreach(var k in RowHeaders)
            {
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
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profiling: Initial heapification of data : End");
#endif
            #endregion

            #region Heap Profiling: Add single element to heap.
#if(PROFILE_ASE)
            Console.WriteLine("Profiling: Add single element to heap : Start");
            //  case 1: add element with heap array resize.
            //  case 2: add element without heap array resize
#if (PROFILE_ASE_C1)
            Console.WriteLine("Profiling: Add single element to heap : Case 1 : Start");
            ColumnHeaders.Add("Profiling: Add single element to heap : Case 1 : add element with heap array resize");
            // Case 1
            iterator = 0;
            foreach(var k in RowHeaders)
            {
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
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }


            Console.WriteLine("Profiling: Add single element to heap : Case 1 : End");
#endif
            //Case 2
#if (PROFILE_ASE_C2)
            Console.WriteLine("Profiling: Add single element to heap : Case 2 : Start");
            ColumnHeaders.Add("Profiling: Add single element to heap : Case 2: add element without heap array resize");
            iterator = 0;
            foreach(var k in RowHeaders)
            {
                A = new RandomArray<int>(k);
                H = new heap<int>(A.data, 2 ^ 31);
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
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }


            Console.WriteLine("Profiling: Add single element to heap : Case 2 : End");
#endif
            Console.WriteLine("Profiling: Add single element to heap : End");
#endif
#endregion
            #region Heap Profiling: Union
#if(PROFILE_UNION)
            Console.WriteLine("Profiling: Set Union : Start");
            // Preconditions: 
            //          No resizing is needed. 
            //          H1.length + H2.length = N
            //          H1 and H2 are disjoined sets
            // Case 1: Union between two heaps
            // Case 1a: Heap H1 is smaller than Heap H2. Ratio < 1:10
            // Case 1b: Heap H1 is larger than Heap H2. Ratio > 10:1
            // Case 1c: Heap H1 is similar in size to Heap H2. Ratio ~ 1:1
            // Case 2: Union between a heap and an Array
#if(PROFILE_UNION_C1)
            Console.WriteLine("Profiling: Set Union : Case 1 : Start");
#if(PROFILE_UNION_C1a)
            Console.WriteLine("Profiling: Set Union : Case 1a : Start");
            ColumnHeaders.Add("Profiling: Add single element to heap : Case 2: add element without heap array resize");
            iterator = 0;
            foreach (var k in RowHeaders)
            {
                generateHeaps(ref H1, ref H2, k, 0.1, 0.1);
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    H1.union(H2);
                    repeats++;
                    // In order to maintain the statistical property for the ratio in the heaps
                    // a new set of heaps within the ration must be generated. In order to 
                    // properly measure the union procedure, the clock will pause for the duration 
                    // of the new set generation.
                    timekeeper.Stop();
                    //    ---------- Stop Timer --------------
                    generateHeaps(ref H1, ref H2, k, 0.1, 0.1);
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling: Set Union : Case 1a : End");
#endif
#if(PROFILE_UNION_C1b)
            Console.WriteLine("Profiling: Set Union : Case 1b : Start");
            Console.WriteLine("Profiling: Set Union : Case 1b : End");
#endif
#if(PROFILE_UNION_C1c)
            Console.WriteLine("Profiling: Set Union : Case 1c : Start");
            Console.WriteLine("Profiling: Set Union : Case 1c : End");
#endif
            Console.WriteLine("Profiling: Set Union : Case 1: End");
#endif
#if(PROFILE_UNION_C2)
            Console.WriteLine("Profiling: Set Union : Case 2 : Start");
            Console.WriteLine("Profiling: Set Union : Case 2 : End");
#endif
#endif
            #endregion
            Console.WriteLine("Profiling: End");

            //printTable(ColumnHeaders, RowHeaders, cellData);

#endif

#endregion

            #region Testing
#if (DEBUG)
            int[] A1 = { 9, 7, 8, 1, 4 };
            int[] A2 = { 2, 6, 3, 19, 0 };
            H1 = new heap<int>(A1, 5);
            H2 = new heap<int>(A2, 5);
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


        public static void printTable(  List<String> ColumnHeaders, 
                                        List<uint> RowHeaders,
                                        List<List<double>> CellData)
        {
            // Declare Excel object to create table
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                Console.WriteLine("Excel is not properly installed!!");
                return;
            }
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = excelApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Sheet 1 content";
            
            xlWorkBook.SaveAs("csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal);
            xlWorkBook.Close(true, misValue, misValue);
            excelApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(excelApp);
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

        /// <summary>
        /// generateHeaps(ref heap<int> H1, ref heap<int> H2, uint N, double r, double stddev)
        ///         Utility:    Generates two (2) heaps with random lengths, such that N = H1.length + H2.length.
        ///                     Given r as the ratio of elements of H1:H2, it will generate heaps of varing
        ///                     lengths within stddev of the specified ratio.
        /// </summary>
        /// <param name="H1">Reference to Heap H1</param>
        /// <param name="H2">Reference to Heap H2</param>
        /// <param name="N">Total number of elements: N = H1.length + H2.length</param>
        /// <param name="r">Ratio of element paritition, H1:H2, r is between 0 and (1-stddev)</param>
        /// <param name="stddev">Standard deviation of the random sample ratio.</param>
        private static void generateHeaps(ref heap<int> H1, ref heap<int> H2, uint N, double r, double stddev=0.5)
        {
            if (stddev < 0 || stddev > 1) throw new ArgumentOutOfRangeException();
            if (r < 0 || r > (1-stddev)) throw new ArgumentOutOfRangeException();

            var ratio =  ((new Random().NextDouble()) * stddev + r);  // Generate a random ratio (≈ r ± 0.05)
            uint S1 = (uint)Math.Floor(ratio*N);
            uint S2 = N - S1;
            RandomArray<int> A = new RandomArray<int>((ulong)S1);
            RandomArray<int> B = new RandomArray<int>((ulong)S2);
            H1 = new heap<int>(A.data, 2 * N);
            H2 = new heap<int>(B.data, 2 * N);
            H1.buildMinHeap();
            H2.buildMinHeap();
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }


}
