using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class BinomialHeap<T>   : IComparable 
                    where T : IComparable
    {
        

        public void link()
        {

        }

        int calculateDegree(heap<T> BHeap)
        {
            return (int)Math.Floor(Math.Log((double)BHeap.length));
        }


    }
}
