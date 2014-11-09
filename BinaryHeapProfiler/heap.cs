using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class heap<T> where T : IComparable<T>
    {
        private T[] nodes;
        public ulong length { set; get; } 
        
        public heap()        {  nodes = new T[length]; }

        public heap(ulong N)
        {
            length = N;
            nodes = new T[length];
        }

        public heap(T[] A, ulong N)
        {
            length = N;
            nodes = new T[N];
            for(ulong x= 0; x<N; x++)
            {
                nodes[x] = A[x];
            }
        }

        public ulong parent(ulong index)
        {
            return (ulong)Math.Floor((Single)index / 2);
        }

        public ulong left(ulong index)
        {
            return (2*index);
        }

        public ulong right(ulong index)
        {
            return (2 * index + 1);
        }

        private void swap(ulong X, ulong Y)
        {
            T swapspace = nodes[X];
            X = Y;
            nodes[Y] = swapspace;
        }

        public void minheapify(ulong index)
        {
            ulong leftchild, rightchild, smallest;
            leftchild = left(index);
            rightchild = right(index);
            if ((leftchild < length) & (nodes[leftchild].CompareTo(nodes[index]) < 0))
                smallest = leftchild;
            else smallest = index;
            if (smallest != index)
            {
                swap(index, smallest);
                minheapify(smallest);
            }
        }
    }
}
