//#define DEBUGING
#define PROFILE
//#define PROFILE_RAP
//#define PROFILE_DAIC
//#define PROFILE_IHOD
//#define PROFILE_GENERATE_SUBSETS
#define PROFILE_GENERATE_SUBSETS_BINARY_HEAPS
#define PROFILE_GENERATE_SUBSETS_BINOMIAL_HEAPS
//#define PROFILE_ASE
#define PROFILE_ASE_C1
#define PROFILE_ASE_C2
//#define PROFILE_UNION
#define PROFILE_UNION_C1
#define PROFILE_UNION_C1a
#define PROFILE_UNION_C1b
#define PROFILE_UNION_C1c
//#define PROFILE_UNION_C2
#define PROFILE_BH
//#define PROFILE_BH_ASE
#define PROFILE_BH_ASE_C1
#define PROFILE_BH_ASE_C2
#define PROFILE_BH_UNION
#define PROFILE_BH_UNION_C1a
#define PROFILE_BH_UNION_C1b
#define PROFILE_BH_UNION_C1c

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
            RandomArray A, B, Un;
            int repeats = 0;
            long Tmax = 2000;
            uint[] N = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int maxPowerN = 4;
            uint iterator=0;
            int Cols = 7;
            double[,] Table = new double[maxPowerN * (10 - 1), Cols];
            double ratio, stddev;
            char[] working = {'-','\\','|', '/'};
            heap<uint> H;
            BinomialHeap<uint> BH = new BinomialHeap<uint>();
            BinomialHeap<uint> BH1 = new BinomialHeap<uint>();
            BinomialHeap<uint> BH2 = new BinomialHeap<uint>();
            heap<uint> H1 = new heap<uint>();
            heap<uint> H2 = new heap<uint>();
            List<String> ColumnHeaders = new List<string>();
            List<uint> RowHeaders = new List<uint>();
            List<List<double>> cellData = new List<List<double>>();
            stddev = 0.1;
            Un = new RandomArray((ulong)(10*(N[N.Length-1]+1)*Math.Pow(10,maxPowerN)));
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
            Console.WriteLine("Profiling : Start");
            #region Random Array Profiling
#if (PROFILE_RAP)
            Console.WriteLine("Profiling : Random Array Profiling : Start");
            ColumnHeaders.Add("Profiling : Random Array Profiling");
            iterator = 0;
            foreach(var k in RowHeaders)
            {
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    A = new RandomArray(k);
                    repeats++;
                }
                timekeeper.Stop();
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds 
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profiling : Random Array Profiling : End");
#endif

#endregion

            #region Heap Profiling: Declaration and Array Initialization copy
#if(PROFILE_DAIC)
            Console.WriteLine("Profiling : Declaration and Array Initialization copy : Start");
            ColumnHeaders.Add("Profiling : Declaration and Array Initialization copy");
            iterator = 0;
            foreach (var k in RowHeaders)
            {
                A = new RandomArray(k);
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    H = new heap<uint>(A.data, k);
                    repeats++;
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds 
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profiling : Declaration and Array Initialization copy : End");
#endif
#endregion

#region Profile: Set Generation
#if(PROFILE_GENERATE_SUBSETS)
            Console.WriteLine("Profile : Set Generation : Start");
#if(PROFILE_GENERATE_SUBSETS_BINARY_HEAPS)
#region Profile : Set Generation : Binary Heaps : Start
            Console.WriteLine("Profile : Set Generation : Binary Heaps : Start");
            ColumnHeaders.Add("Profile : Set Generation : Binary Heaps : Start");
            iterator = 0;
            foreach (var k in RowHeaders)
            {
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    generateHeaps(ref H1, ref H2, k, 0.5, 0.25);
                    repeats++;
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profile : Set Generation : Binary Heaps : End");
#endregion // Profile: Set Generation: Binary Heaps: End
#endif  // PROFILE_GENERATE_SUBSETS_BINARY_HEAPS

#if(PROFILE_GENERATE_SUBSETS_BINOMIAL_HEAPS)
#region Profile: Set Generation: Binomial Heaps: Start
            Console.WriteLine("Profile : Set Generation : Binomial Heaps : Start");
            ColumnHeaders.Add("Profile : Set Generation : Binomial Heaps : Start");
            iterator = 0;
            foreach (var k in RowHeaders)
            {
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    generateBinomialHeaps(ref BH1, ref BH2, k, 0.5, 0.25);
                    repeats++;
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }

            Console.WriteLine("Profile : Set Generation : Binomial Heaps : End");
#endregion // Profile: Set Generation: Binomial Heaps: End
#endif  // PROFILE_GENERATE_SUBSETS_BINOMIAL_HEAPS
            Console.WriteLine("Profile : Set Generation : End");
#endif //PROFILE_GENERATE_SUBSETS
#endregion


            #region Heap Profiling: Initial heapification of data.
#if(PROFILE_IHOD)
            Console.WriteLine("Profiling : Binary Heap : Initial heapification of data : Start");
            ColumnHeaders.Add("Profiling : Binary Heap : Initial heapification of data");
            iterator = 0;
            foreach(var k in RowHeaders)
            {
                A = new RandomArray(k);
                H = new heap<uint>(A.data, k);
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

            Console.WriteLine("Profiling : Binary Heap : Initial heapification of data : End");
#endif
            #endregion

            #region Heap Profiling: Add single element to heap.
#if(PROFILE_ASE)
            Console.WriteLine("Profiling : Binary Heap : Add single element to heap : Start");
            //  case 1: add element with heap array resize.
            //  case 2: add element without heap array resize
#if (PROFILE_ASE_C1)
            Console.WriteLine("Profiling : Binary Heap : Add single element to heap : Case 1 : Start");
            ColumnHeaders.Add("Profiling : Binary Heap : Add single element to heap : Case 1 : add element with heap array resize");
            // Case 1
            iterator = 0;
            foreach(var k in RowHeaders)
            {
                A = new RandomArray(k);
                H = new heap<uint>(A.data, k);
                Random randValue = new Random();
                H.buildMinHeap();
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    // In order to maintain the statistical property for operation a new heap
                    // must be generated. In order to properly measure the insertion procedure 
                    // with the array resize. The clock will pause for the duration 
                    // of the new heap generation.
                    H.insertElement((uint)randValue.Next());
                    repeats++;
                    timekeeper.Stop();
                    //    ---------- Stop Timer --------------
                    H = new heap<uint>(A.data, k);
                    H.buildMinHeap();
                    //Console.Write(working[(int)(repeats / 1000) % working.Length] + "\b");
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }


            Console.WriteLine("Profiling : Binary Heap : Add single element to heap : Case 1 : End");
#endif
            //Case 2
#if (PROFILE_ASE_C2)
            Console.WriteLine("Profiling : Binary Heap : Add single element to heap : Case 2 : Start");
            ColumnHeaders.Add("Profiling : Binary Heap : Add single element to heap : Case 2: add element without heap array resize");
            iterator = 0;
            foreach(var k in RowHeaders)
            {
                A = new RandomArray(k);
                H = new heap<uint>(A.data, 2 ^ 31);
                Random randValue = new Random();
                H.buildMinHeap();
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    H.insertElement((uint)randValue.Next());
                    repeats++;

                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }


            Console.WriteLine("Profiling : Binary Heap : Add single element to heap : Case 2 : End");
#endif
            Console.WriteLine("Profiling : Binary Heap : Add single element to heap : End");
#endif
            #endregion

            #region Heap Profiling: Union
#if(PROFILE_UNION)
            #region Profiling: Set Union : Start
            Console.WriteLine("Profiling : Binary Heap : Set Union : Start");
            // Preconditions: 
            //          No resizing is needed. 
            //          H1.length + H2.length = N
            //          H1 and H2 are disjoined sets
            // Case 1: Union between two heaps
            // Case 1a: Heap H1 is smaller than Heap H2. Ratio ~ 1:10
            // Case 1b: Heap H1 is larger than Heap H2. Ratio ~ 10:1
            // Case 1c: Heap H1 is similar in size to Heap H2. Ratio ~ 1:1
            
#if(PROFILE_UNION_C1)
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1 : Start");
#if(PROFILE_UNION_C1a)
            #region Profiling: Set Union : Binary Heap : Case 1a : Start
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1a : Start");
            ColumnHeaders.Add("Profiling : Binary Heap : Case 1a : Heap H1 is smaller than Heap H2. Ratio ~ 1:10");
            iterator = 0;
            ratio = 0.1;
            foreach (var k in RowHeaders)
            {
                generateHeaps(ref H1, ref H2, k, ratio, stddev);
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
                    generateHeaps(ref H1, ref H2, k, ratio, stddev);
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1a : End");
            #endregion // Profiling: Set Union : Case 1a : End
#endif // PROFILE_UNION_C1a
#if(PROFILE_UNION_C1b)
            #region Profiling: Set Union : Binary Heap : Case 1b : Start
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1b : Start");
            ColumnHeaders.Add("Profiling : Binary Heap : Case 1b : Heap H1 is larger than Heap H2. Ratio ~ 10:1");
            iterator = 0;
            ratio = 0.9;
            foreach (var k in RowHeaders)
            {
                generateHeaps(ref H1, ref H2, k, ratio, stddev);
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
                    generateHeaps(ref H1, ref H2, k, ratio, stddev);
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1b : End");
            #endregion // Profiling: Set Union : Case 1b : End
#endif // PROFILE_UNION_C1b
#if(PROFILE_UNION_C1c)
            #region Profiling: Set Union : Binary Heap : Case 1c : Start
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1c : Start");
            ColumnHeaders.Add("Profiling : Binary Heap : Case 1c : Heap H1 is similar in size to Heap H2. Ratio ~ 1:1");
            iterator = 0;
            ratio = 0.5;
            foreach (var k in RowHeaders)
            {
                generateHeaps(ref H1, ref H2, k, ratio, stddev);
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
                    generateHeaps(ref H1, ref H2, k, ratio, stddev);
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1c : End");
            #endregion // Profiling: Set Union : Case 1c : End
#endif // PROFILE_UNION_C1c
            Console.WriteLine("Profiling : Binary Heap : Set Union : Case 1: End");
#endif

            Console.WriteLine("Profiling : Binary Heap : Set Union : End");
            #endregion // Profiling: Set Union : End
#endif
            #endregion

#if(PROFILE_BH)
            #region Binomial Heap Profiling
            Console.WriteLine("Profiling : Binomial Heap: Start");
#if(PROFILE_BH_ASE)
            #region Binomial Heap Profiling: Add single elent
            Console.WriteLine("Profiling : Binomial Heap: Add Single Element : Start");
#if(PROFILE_BH_ASE_C1)
            #region Binomial Heap Profilinag: Add single element
            Console.WriteLine("Profiling : Binomial Heap : Add Single Element : Start");

            ColumnHeaders.Add("Profiling : Binomial Heap : Add Single Element");
            // Case 1
            iterator = 0;
            foreach (var k in RowHeaders)
            {
                A = new RandomArray(k);
                BH = new BinomialHeap<uint>();
                Random randValue = new Random();
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    BH.HeapInsert(randValue.Next());
                    repeats++;
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling : Binomial Heap : Add Single Element : End");
            #endregion // Binomial Heap Profilinag: Add single element
#endif  // PROFILE_BH_ASE_C1

            #endregion // Binomial Heap Profiling: Add single elent
#endif  // PROFILE_BH_ASE

#if(PROFILE_BH_UNION)
            #region Binomial Heap Profiling: UNION
            // Preconditions: 
            //          No resizing is needed. 
            //          H1.length + H2.length = N
            //          H1 and H2 are disjoined sets
            // Case 1: Union between two Binomial Heaps
            // Case 1a: Heap H1 is smaller than Heap H2. Ratio ~ 1:10
            // Case 1b: Heap H1 is larger than Heap H2. Ratio ~ 10:1
            // Case 1c: Heap H1 is similar in size to Heap H2. Ratio ~ 1:1
            Console.WriteLine("Profiling: Binomial Heap: Set Union : Start");

            #region Binomial Heap Profiling: Set Union : Case 1a : Start
#if(PROFILE_BH_UNION_C1a)
            Console.WriteLine("Profiling: Binomial Heap: Set Union : Case 1a : Start");
            ColumnHeaders.Add("Profiling: Binomial Heap: Set Union : Case 1a : Heap H1 is smaller than Binomial Heap H2. Ratio ~ 1:10");
            iterator = 0;
            ratio = 0.1;
            foreach (var k in RowHeaders)
            {
                generateBinomialHeaps(ref BH1, ref BH2, k, ratio, stddev);
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    BH1 = BH1.Binomial_HeapUnion(BH1,BH2);
                    repeats++;
                    // In order to maintain the statistical property for the ratio in the heaps
                    // a new set of Binomial heaps within the ration must be generated. In order to 
                    // properly measure the union procedure, the clock will pause for the duration 
                    // of the new set generation.
                    timekeeper.Stop();
                    //    ---------- Stop Timer --------------
                    /*
                    var loopratio = (((new Random().NextDouble()) - 0.5) * stddev + 0.1);  // Generate a random ratio (≈ r ± stddev/2)
                    var displace = (uint)(new Random().Next(0,(int)(81 * Math.Pow(10, maxPowerN))));
                    uint S1 = displace + (uint)Math.Floor(loopratio * k);
                    uint S2 = displace + k - S1;
                    BH1 = new BinomialHeap<uint>();
                    BH1.HeapInsert(Un.data, displace, S1);
                    BH1 = new BinomialHeap<uint>();
                    BH1.HeapInsert(Un.data, S2, k);
                    */
                    generateBinomialHeaps(ref BH1, ref BH2, k, ratio, stddev);
                    if(repeats%500==0)
                        Console.Write(working[(int)(repeats/100 ) % working.Length] + "\b");
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
#endif // PROFILE_BH_UNION_C1a
            #endregion // Binomial Heap Profiling: Set Union : Case 1a : End

            #region Profiling: Set Union : Binomial Heap : Case 1b : Start
#if(PROFILE_BH_UNION_C1b)
            Console.WriteLine("Profiling : Binomial Heap : Set Union : Case 1b : Start");
            ColumnHeaders.Add("Profiling : Binomial Heap : Case 1b : Heap H1 is larger than Binomial Heap H2. Ratio ~ 10:1");
            iterator = 0;
            ratio = 0.9;
            foreach (var k in RowHeaders)
            {
                generateBinomialHeaps(ref BH1, ref BH2, k, ratio, stddev);
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    // In order to maintain the statistical property for the ratio in the heaps
                    // a new set of Binomial heaps within the ration must be generated. In order to 
                    // properly measure the union procedure, the clock will pause for the duration 
                    // of the new set generation.
                    timekeeper.Stop();
                    //    ---------- Stop Timer --------------
                    /*
                    var loopratio = (((new Random().NextDouble()) - 0.5) * stddev + 0.9);  // Generate a random ratio (≈ r ± stddev/2)
                    var displace = (uint)(new Random().Next(0, (int)(81 * Math.Pow(10, maxPowerN))));
                    uint S1 = displace + (uint)Math.Floor(loopratio * k);
                    uint S2 = displace + k - S1;
                    BH1 = new BinomialHeap<uint>();
                    BH1.HeapInsert(Un.data, displace, S1);
                    BH1 = new BinomialHeap<uint>();
                    BH1.HeapInsert(Un.data, S2, k);
                    */
                    generateBinomialHeaps(ref BH1, ref BH2, k, ratio, stddev);
                    if (repeats % 500 == 0)
                        Console.Write(working[(int)(repeats / 100) % working.Length] + "\b");
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling : Binomial Heap : Set Union : Case 1b : End");
#endif // PROFILE_BH_UNION_C1b
            #endregion // Profiling: Set Union : Binomial Heap : Case 1b :End

            #region Profiling: Set Union : Binomial Heap : Case 1c : Start
#if(PROFILE_BH_UNION_C1c)
            Console.WriteLine("Profiling : Binomial Heap : Set Union : Case 1c : Start");
            ColumnHeaders.Add("Profiling : Binomial Heap : Case 1b : Heap H1 is similar to Binomial Heap H2. Ratio ~ 1:1");
            iterator = 0;
            ratio = 0.5;
            foreach (var k in RowHeaders)
            {
                generateBinomialHeaps(ref BH1, ref BH2, k, ratio, stddev);
                // Profiling Starts
                repeats = 0;
                timekeeper.Restart();
                while (timekeeper.ElapsedMilliseconds < Tmax)
                {
                    BH1 = BH1.Binomial_HeapUnion(BH1, BH2);
                    repeats++;
                    // In order to maintain the statistical property for the ratio in the heaps
                    // a new set of Binomial heaps within the ration must be generated. In order to 
                    // properly measure the union procedure, the clock will pause for the duration 
                    // of the new set generation.
                    timekeeper.Stop();
                    //    ---------- Stop Timer --------------
                    /*
                    var loopratio = (((new Random().NextDouble()) - 0.5) * stddev + 0.5);  // Generate a random ratio (≈ r ± stddev/2)
                    var displace = (uint)(new Random().Next(0, (int)(81 * Math.Pow(10, maxPowerN))));
                    uint S1 = displace + (uint)Math.Floor(loopratio * k);
                    uint S2 = displace + k - S1;
                    BH1 = new BinomialHeap<uint>();
                    BH1.HeapInsert(Un.data, displace, S1);
                    BH1 = new BinomialHeap<uint>();
                    BH1.HeapInsert(Un.data, S2, k);
                    */
                    generateBinomialHeaps(ref BH1, ref BH2, k, ratio, stddev);
                    if (repeats % 500 == 0)
                        Console.Write(working[(int)(repeats / 100) % working.Length] + "\b");
                    //    --------- Start Timer --------------
                    timekeeper.Start();
                }
                timekeeper.Stop();
                // Profiling ends
                cellData.ElementAt((int)(iterator++)).Add((double)timekeeper.ElapsedMilliseconds
                                        / ((double)repeats * 1000));
                Console.WriteLine("Done: N=" + k.ToString());
            }
            Console.WriteLine("Profiling : Binomial Heap : Set Union : Case 1c : End");
#endif // PROFILE_BH_UNION_C1c
            #endregion // Profiling: Set Union : Binomial Heap : Case 1c :End

            Console.WriteLine("Profiling: Binomial Heap: Set Union: End");
            #endregion // Binomial Heap Profiling: UNION
#endif // PROFILE_BH_UNION

            Console.WriteLine("Profiling: Binomial Heap: End");
            #endregion  //Binomial Heap Profiling

#endif // PROFILE_BH
            Console.WriteLine("Profiling: End");
            printTable(ColumnHeaders, RowHeaders, cellData);
#endif // PROFILE

#endregion // Profiling Code

            #region Testing
#if (DEBUGING)
            int[]  A1 = { 9, 7, 8, 1, 4 };
            uint[] A2 = { 2, 6, 3, 19, 0 };
            int[]  A3 = { 2, 6, 3, 19, 42, 73, 91, 111 };
            H1 = new heap<uint>(A2, 5);
            H2 = new heap<uint>(A2, 5);
            BH  = new BinomialHeap<uint>();
            BH1 = new BinomialHeap<uint>();
            BH2 = new BinomialHeap<uint>();
            Console.WriteLine("****** H1 ******");
            BH1.HeapInsert(A1);
            Console.WriteLine("Degree of HB1 is " + BH1.getDegree().ToString());
            BH1.print();
            Console.WriteLine("****** H2 ******");
            BH2.HeapInsert(A3);
            Console.WriteLine("Degree of HB2 is " + BH2.getDegree().ToString());
            BH2.print();
            Console.WriteLine("****** H1UH2 ******");
            BH = BH.Binomial_HeapUnion(BH1, BH2);
            Console.WriteLine("Degree of HB is " + BH.getDegree().ToString());
            BH.print();
            
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
                Console.WriteLine("Excel is not properly installed. Aborting operation.");
                return;
            }
         
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = excelApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "Data log from Profiling.";
                int r, c;
                r = 3; c = 2;
                foreach (var col in ColumnHeaders)
                {
                    xlWorkSheet.Cells[2, c] = ColumnHeaders.ElementAt(c-2);
                    foreach (var row in RowHeaders)
                    {
                        if (c == 2)
                        {
                            xlWorkSheet.Cells[r, 1] = RowHeaders.ElementAt(r-3);
                        }
                        xlWorkSheet.Cells[r, c] = CellData.ElementAt(r-3).ElementAt(c-2);
                        r++;
                    }
                    r = 3;
                    c++;
                }
                string now = "(" + System.DateTime.Now.Year.ToString()
                                 + System.DateTime.Now.Month.ToString()
                                 + System.DateTime.Now.Day.ToString()
                                 + System.DateTime.Now.Hour.ToString()
                                 + System.DateTime.Now.Minute.ToString()
                                 + System.DateTime.Now.Second.ToString()
                                 +   ")";
                xlWorkBook.SaveAs(now+" Profiling.xls", Excel.XlFileFormat.xlWorkbookNormal);
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
        /// generateHeaps(ref heap<uint> H1, ref heap<uint> H2, uint N, double r, double stddev)
        ///         Utility:    Generates two (2) heaps with random lengths, such that N = H1.length + H2.length.
        ///                     Given r as the ratio of elements of H1:H2, it will generate heaps of varing
        ///                     lengths within stddev of the specified ratio.
        /// </summary>
        /// <param name="H1">Reference to Heap H1</param>
        /// <param name="H2">Reference to Heap H2</param>
        /// <param name="N">Total number of elements: N = H1.length + H2.length</param>
        /// <param name="r">Ratio of element paritition, H1:H2, r is between 0 and (1-stddev)</param>
        /// <param name="stddev">Standard deviation of the random sample ratio.</param>
        private static void generateHeaps(ref heap<uint> H1, ref heap<uint> H2, uint N, double r, double stddev=0.5)
        {
            if (stddev < 0 || stddev > 1) throw new ArgumentOutOfRangeException();
            if (r < 0 || r > (1-stddev)) throw new ArgumentOutOfRangeException();

            var ratio = (((new Random().NextDouble()) - 0.5) * stddev + r);  // Generate a random ratio (≈ r ± stddev/2)
            uint S1 = (uint)Math.Floor(ratio*N);
            uint S2 = N - S1;
            RandomArray A = new RandomArray((ulong)N);
            H1 = new heap<uint>(A.data, 0, S1, 2 * N);
            H2 = new heap<uint>(A.data, S2, N,  2 * N);
            H1.buildMinHeap();
            H2.buildMinHeap();
        }

        /// <summary>
        /// generateHeaps(ref heap<uint> H1, ref BinomialHeap<uint> H2, BinomialHeap, N, double r, double stddev)
        ///         Utility:    Generates two (2) heaps with random lengths, such that N = H1.length + H2.length.
        ///                     Given r as the ratio of elements of H1:H2, it will generate heaps of varing
        ///                     lengths within stddev of the specified ratio.
        /// </summary>
        /// <param name="H1">Reference to Binomial Heap H1</param>
        /// <param name="H2">Reference to Binomial Heap H2</param>
        /// <param name="N">Total number of elements: N = H1.length + H2.length</param>
        /// <param name="r">Ratio of element paritition, H1:H2, r is between 0 and (1-stddev)</param>
        /// <param name="stddev">Standard deviation of the random sample ratio.</param>
        private static void generateBinomialHeaps(ref BinomialHeap<uint> BH1, ref BinomialHeap<uint> BH2, uint N, double r, double stddev = 0.5)
        {
            if (stddev < 0 || stddev > 1) throw new ArgumentOutOfRangeException();
            if (r < 0 || r > (1 - stddev)) throw new ArgumentOutOfRangeException();

            var ratio = (((new Random().NextDouble()) - 0.5) * stddev + r);  // Generate a random ratio (≈ r ± stddev/2)
            uint S1 = (uint)Math.Floor(ratio * N);
            uint S2 = N - S1;
            RandomArray A = new RandomArray((ulong)N);
            BH1 = new BinomialHeap<uint>();
            BH1.HeapInsert(A.data, (uint)0, S1);
            BH2 = new BinomialHeap<uint>();
            BH2.HeapInsert(A.data, S2, N);
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
