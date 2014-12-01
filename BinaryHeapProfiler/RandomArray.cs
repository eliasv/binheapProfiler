using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class RandomArray
    {
        public uint[] data;
        public ulong length { get; set; }
        private Random RandomVariable;
        public object this[int i] 
        { 
            get { return data[i]; }
        }

        public RandomArray(ulong Size)
        {
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

        private void swap(ulong A, ulong B)
        {
            var tmp = data[A];
            data[A] = data[B];
            data[B] = tmp;
        }

        private bool repeated(uint t, ulong len)
        {
            for (ulong i = 0; i < (len); i++)
                if ((data[i].CompareTo(t))==0) return true;
            return false;

        }

       public void print()
        {

            Console.WriteLine("Array = ");
            for (ulong i = 0; i < length; i++)
                Console.WriteLine("{0}", data[i]);
            Console.ReadLine();
        }
    }
}
