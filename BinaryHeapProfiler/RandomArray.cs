using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class RandomArray<T> where T : IComparable<T>
    {
        public T[] data;
        public ulong length { get; set; }
        private Random RandomVariable;
        public object this[int i] 
        { 
            get { return data[i]; }
        }

        public RandomArray(ulong Size)
        {
            length = Size;
            data = new T[length];
            RandomVariable = new Random();
            for (ulong i = 0; i < length; i++)
            {
                while (repeated(data[i], i))
                {
                    if (typeof(T) == typeof(int))
                        data[i] = (T)(Object)RandomVariable.Next();
                    else if (typeof(T) == typeof(double))
                        data[i] = (T)(Object)RandomVariable.NextDouble();
                    else
                        break;
                }
            }
        }

        private bool repeated(T t, ulong len)
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
