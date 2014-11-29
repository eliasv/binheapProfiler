using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class BinomialHeap<T> where T : IComparable
    {
        int calculateDegree(heap<T> BHeap)
        {
            return (int)Math.Ceiling(Math.Log((double)BHeap.length));
        }
    }
}
