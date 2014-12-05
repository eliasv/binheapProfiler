using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    /// <summary>
    /// Support class that generates random sets of values. The arrays
    /// contain numbers from 1 to N. In order to properly randomize the 
    /// array, it is randomly index swapped.
    /// 
    ///         Data members:
    ///             - data      : Array with the Set of randomized values.
    ///             - length    : Length of the array.
    ///             
    ///         Mothods:
    ///             - this[int]     : Overload for the array index operator.
    ///             - RandomArray(ulong)
    ///                             : Constructor. Generates an array of random values.
    ///             - swap(ulong, ulong)
    ///                             : Performs an index swap between the two given indices.
    ///             - repeated(uint, ulong)
    ///                             : Searches the array for repeated values. Depricated Method.
    ///             - print()       : Visualization of the Randomized array. Used for debugging purposes.
    ///             
    /// 
    /// </summary>
    class RandomArray
    {
        /// <summary>
        /// Array with the Set of randomized values.
        /// </summary>
        public uint[] data;

        /// <summary>
        /// Length of the array.
        /// </summary>
        public ulong length { get; set; }

        /// <summary>
        /// this[int]
        /// 
        /// Overload for the array index operator.
        /// </summary>
        /// <param name="i">Index to retrieve.</param>
        /// <returns></returns>
        public object this[int i] 
        { 
            get { return data[i]; }
        }

        /// <summary>
        /// RandomArray(ulong)
        /// 
        /// Constructor. Generates an array of random values.
        /// </summary>
        /// <param name="Size">Size of the desired array.</param>
        public RandomArray(ulong Size)
        {
            Random RandomVariable;
            length = Size;
            data = new uint[length];
            RandomVariable = new Random();
            for (ulong i = 0; i < length; i++)
            {
                data[i] = (uint)i + 1;
            }
            for (ulong i = 0; i < length; i++)
            {
                swap(i,(ulong)RandomVariable.Next(0, (int)length));
            }

        }

        /// <summary>
        /// swap(ulong, ulong)
        /// 
        /// Performs an index swap between the two given indices.
        /// </summary>
        /// <param name="A">First index for swap.</param>
        /// <param name="B">Second index for swap.</param>
        private void swap(ulong A, ulong B)
        {
            var tmp = data[A];
            data[A] = data[B];
            data[B] = tmp;
        }

        /// <summary>
        /// repeated(uint, ulong)
        /// 
        /// Searches the array for repeated values. Depricated Method.
        /// </summary>
        /// <param name="t"> Value to search for.</param>
        /// <param name="len">Number of elements to search. Starting from first element.</param>
        /// <returns></returns>
        private bool repeated(uint t, ulong len)
        {
            for (ulong i = 0; i < (len); i++)
                if ((data[i].CompareTo(t))==0) return true;
            return false;

        }

        /// <summary>
        /// print()
        /// 
        /// Visualization of the Randomized array. Used for debugging purposes.
        /// </summary>
        public void print()
        {

            Console.WriteLine("Array = ");
            for (ulong i = 0; i < length; i++)
                Console.WriteLine("{0}", data[i]);
            Console.ReadLine();
        }
    }
}
