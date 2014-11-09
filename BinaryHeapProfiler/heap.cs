using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class heap<T> where T : IComparable<T>
    {
        public T[] nodes;
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
            nodes = new T[N+1];
            for(ulong x= 1; x<N+1; x++)
            {
                nodes[x] = A[x-1];
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
            nodes[X] = nodes[Y];
            nodes[Y] = swapspace;
        }

        public void minheapify(ulong index)
        {
            ulong parentnode, leftchild, rightchild, smallest = index;
            leftchild = left(index);
            rightchild = right(index);
            parentnode = parent(index);
            if ((leftchild <= length) && (nodes[leftchild].CompareTo(nodes[index]) < 0))
                smallest = leftchild;
            if ((rightchild <= length) && (nodes[rightchild].CompareTo(nodes[smallest]) < 0))
                smallest = rightchild;
            //else smallest = index;
            if (smallest != index)
            {
                swap(index, smallest);
                if(parentnode>0)
                    minheapify(parent(index));
            }
        }

        public void buildMinHeap()
        {
            for (ulong i= (ulong)((double)length/2); i > 0; i--)
                minheapify(i);
        }

        public void print()
        {
            for (ulong i = 1; i < length+1; i++)
                Console.WriteLine(nodes[i]);
            Console.WriteLine();
        }
    }
}
