using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    /// <summary>
    /// Min Heap Data Structured based on CLRS 3rd Edition
    ///     Implementation by: Elias V. Beauchamp Rodriguez
    ///                        As a requirement for the course COMP6785
    ///     A Min Heap is a nearly complete binary tree that maintains the MinHeap Property
    ///         - For all nodes except for the root, Parent(key) <= Children(key)
    ///         - For any node located at index i:
    ///             - Left Child is located at index:   (2*i)
    ///             - Right Child is located at index:  (2*i+1)
    ///             - Parent node is located at index:  (floor(i/2))
    ///     
    ///     The structure uses a Generic as a type for its array it can be used as a template
    ///     class for any and all objects that implement the IComparable interface. Although 
    ///     it increases overhead and as such its performance, it greatly increases its
    ///     reusability and portability to other Object Oriented Languages.
    ///     
    ///     The following methods have been implemented:
    ///         - heap()                - Defaut Constructor: Generates an empty heap with a defualt size.
    ///         - heap(ulong)           - Constructor: Generates an empty heap with a given size.
    ///         - heap(T[], ulong)      - Constructor: Generates a heap from the data in an array. The initial
    ///                                                size of the heap is the square of the size of the array
    ///                                                in order to allow for the heap to grow as necesary.
    ///         - parent(ulong)         - Method that determines the the parent of any node in the heap.
    ///         - left(ulong)           - Method that determines the the left child of any node in the heap.
    ///         - right(ulong)          - Method that determines the the right child of any node in the heap.
    ///         - swap(ulong, ulong)    - Method that swap the data in the heap array for two (2) gived indices.
    ///         - minHeapify(ulong)     - Method that maintains the heap property on any given node.
    ///         - buildMinheap()        - Method used to initiate heap maintenance. Checks on the heap 
    ///                                                structure from the root.
    ///         - buildMinHeap(ulong)   - Method used for maintaining the heap property of any given node. Given 
    ///                                                node i, it will verify that the heap property is maintained
    ///                                                and will recursively call on itself from the top-down. Uses
    ///                                                minHeapify(ulong) to maintain the heap property.
    ///         - print()               - Method used to print the heap to the Console. This method is used for 
    ///                                                 debugging and is not meant to be used outside of that 
    ///                                                 purpose.
    ///         
    /// </summary>
    /// <typeparam name="T">Any data type T that implements the IComparable interface.</typeparam>
    class heap<T> where T : IComparable<T>
    {
        /// <summary>
        /// Data members
        ///     This section provides the declarations for all the heap data members
        ///     and the necesary objects for the data structure's propper maintenance.
        ///         - nodes:    array of objects to be used in the heap. It is defined as 
        ///                     array of generics so that it can be extended to any and
        ///                     all objects that implement the IComparable interface.
        ///         - length:   (unsigned long) used as the delimiter for the active 
        ///                     heap structure.
        ///         - Size:     (unsigned long) used as the delimiter for the active array
        ///                     used in the heap.
        /// </summary>
        public T[] nodes;
        public ulong length { set; get; }
        public ulong Size { get; set; }
        /// <summary>
        /// heap()
        ///     Defaut Constructor: Generates an empty heap with a defualt size.
        /// </summary>
        public heap() { Size = 50; nodes = new T[Size]; }

        /// <summary>
        /// heap(ulong N)
        ///         Constructor: Generates an empty heap with a given size N.
        /// </summary>
        /// <param name="N">Initial size of the desired heap's array.</param>
        public heap(ulong N)
        {
            length = N;
            Size = N * N;
            nodes = new T[Size];
        }

        /// <summary>
        /// heap(T[], ulong)
        ///         Constructor: Generates a heap from the data in an array. The 
        ///         initial size of the heap is the square of the size of the array 
        ///         in order to allow for the heap to grow as necesary.
        /// </summary>
        /// <param name="A">Data to be used to populate the initial heap array.</param>
        /// <param name="N">Length of array A.</param>
        public heap(T[] A, ulong N)
        {
            length = N;
            Size = N * N;
            nodes = new T[Size];
            for(ulong x= 1; x<N+1; x++)
            {
                nodes[x] = A[x-1];
            }
        }

        /// <summary>
        /// parent(ulong)
        ///         Method that determines the the parent of any node in the heap.
        /// </summary>
        /// <param name="index">Index of the node for which to extract its parent.</param>
        /// <returns>Index of the parent node given by the input.</returns>
        public ulong parent(ulong index)       { return (index >> 1);  }

        /// <summary>
        /// left(ulong)
        ///         Method that determines the the left child of any node in the heap.
        /// </summary>
        /// <param name="index">Index of the node for which to extract its left child.</param>
        /// <returns>Index of the left child's node given by the input.</returns>
        public ulong left(ulong index)         { return (index<<1);  }

        /// <summary>
        /// right(ulong)
        ///         Method that determines the the right child of any node in the heap.
        /// </summary>
        /// <param name="index">Index of the node for which to extract its right child.</param>
        /// <returns>Index of the right child's node given by the input.</returns>
        public ulong right(ulong index)        { return ((index<<1) + 1); }

        /// <summary>
        /// swap(ulong, ulong)
        ///         Method that swap the data in the heap array for two (2) gived indices.
        /// </summary>
        /// <param name="X">Index of one of the first object to swap in the heap array.</param>
        /// <param name="Y">Index of one of the second object to swap in the heap array.</param>
        private void swap(ulong X, ulong Y)
        {
            T swapspace = nodes[X];
            nodes[X] = nodes[Y];
            nodes[Y] = swapspace;
           // Console.WriteLine("Swapped: A[" + X + "] with A[" +Y+"]");
           // print();
        }

        /// <summary>
        /// minHeapify(ulong)
        ///         Method that maintains the heap property on any given node.
        /// </summary>
        /// <param name="index">Node in which to maintain the min-heap property.</param>
        public void minHeapify(ulong index)
        {
            ulong parentnode, leftchild, rightchild, smallest = index;
            leftchild = left(index);
            rightchild = right(index);
            parentnode = parent(index);
            //Console.WriteLine("minheapifying(" + index + ")");
            if ((leftchild <= length) && (nodes[leftchild].CompareTo(nodes[index]) < 0))
                smallest = leftchild;
            if ((rightchild <= length) && (nodes[rightchild].CompareTo(nodes[smallest]) < 0))
                smallest = rightchild;
            if (smallest != index)
            {
                swap(index, smallest);
                minHeapify(smallest);
            }
        }

        /// <summary>
        /// buildMinheap()
        ///         Method used to initiate heap maintenance. Checks on the heap structure from the root.
        /// </summary>
        public void buildMinHeap()
        {
            buildMinHeap(1);

        }

        /// <summary>
        /// buildMinHeap(ulong)
        ///         Method used for maintaining the heap property of any given node. Given node i, it 
        ///         will verify that the heap property is maintained and will recursively call on itself 
        ///         from the top-down. Uses minHeapify(ulong) to maintain the heap property.
        /// </summary>
        /// <param name="i"></param>
        private void buildMinHeap(ulong i)
        {
            if (left(i).CompareTo(length) < 0)
                buildMinHeap(left(i));
            if (right(i).CompareTo(length) < 0)
                buildMinHeap(right(i));
            minHeapify(i);
        }

        /// <summary>
        /// print()
        ///         Method used to print the heap to the Console. This method is used for debugging 
        ///         and is not meant to be used outside of that purpose.
        /// </summary>
        public void print()
        {
            Console.WriteLine("------------------");
            for (ulong i = 1; i < length+1; i++)
                Console.WriteLine("A[{0}] = {1}",i,nodes[i]);
            Console.WriteLine("------------------");
        }
    }
}
