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
    ///         - heap(T[], ulong)      - Constructor: Constructor: Generates a heap from the data in an 
    ///                                             array. The  initial size of the heap is given by the 
    ///                                             parameter N. IF N is smaller than the input array A, 
    ///                                             the length of array A is used instead.
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
    ///         - insertElement()       - Method used to insert an element into the heap. The method heapifies the 
    ///                                                 parent node in order to maintain the min heap property.
    ///         - resizeHeap(uint)      - Method to resize the heap array to a new size. The method creates a new
    ///                                                 array and copies the elements of the old array into it.
    ///                                                 The new object is then used to replace the old reference.
    ///         - union(heap)           - Method for the union of two(2) heaps. If the current heap does not 
    ///                                                 have enough room to hold the second heap, the current 
    ///                                                 heap is rezied in order for both heaps to be allocated 
    ///                                                 into the current heap. The data from the second heap is 
    ///                                                 then copied to the current heap, buildMinHeap is called 
    ///                                                 to maintain the min heap property.
    ///         - union(T[])            - Method for the union of a heap with an array of the same data type. 
    ///                                                 If the current heap does not have enough room to hold 
    ///                                                 the data from the array, the current heap is rezied in 
    ///                                                 order for data to be allocated into the current heap. 
    ///                                                 The data from the array is then copied to the 
    ///                                                 current heap, buildMinHeap is called to maintain the 
    ///                                                 min heap property.
    ///         Method is private because it does not maintain the Min Heap property and 
    ///         could be used to distabilized the heap.
    ///         
    /// </summary>
    /// <typeparam name="T">Any data type T that implements the IComparable interface.</typeparam>
    class heap<T> where T : IComparable<T>
    {
        // 
        // Data members
        //     This section provides the declarations for all the heap data members
        //     and the necesary objects for the data structure's propper maintenance.
        //         - nodes:    Array of objects to be used in the heap. It is defined as 
        //                     array of generics so that it can be extended to any and
        //                     all objects that implement the IComparable interface.
        //         - length:   (unsigned int) Used as the delimiter for the active 
        //                     heap structure.
        //         - Size:     (unsigned int) Used as the delimiter for the active array
        //                     used in the heap.
        // 


        /// <summary>
        /// nodes:      Array of objects to be used in the heap. It is defined as array of 
        ///             generics so that it can be extended to any and all objects that 
        ///             implement the IComparable interface.
        /// </summary>
        public T[] nodes;
        /// <summary>
        /// length:     (unsigned int) Used as the delimiter for the active heap structure.
        /// </summary>
        public uint length { set; get; }
        /// <summary>
        /// Size:       (unsigned int) Used as the delimiter for the active array used in 
        ///             the heap.
        /// </summary>
        public uint Size { get; set; }
        /// <summary>
        /// heap()
        ///     Defaut Constructor: Generates an empty heap with a defualt size.
        /// </summary>
        public heap() { Size = 50; nodes = new T[Size]; }

        /// <summary>
        /// heap(uint N)
        ///         Constructor: Generates an empty heap with a given size N.
        /// </summary>
        /// <param name="N">Initial size of the desired heap's array.</param>
        public heap(uint N)
        {
            length = N;
            Size = N * N;
            nodes = new T[Size];
        }

        /// <summary>
        /// heap(T[], uint)
        ///         Constructor: Generates a heap from the data in an array. The 
        ///         initial size of the heap is given by the parameter N. IF N is
        ///         smaller than the input array A, the length of array A is used 
        ///         instead.
        /// </summary>
        /// <param name="A">Data to be used to populate the initial heap array.</param>
        /// <param name="N">Initial size of heap.</param>
        public heap(T[] A, uint N)
        {

            length = (uint)A.Length;
            if (length <= N)
                Size = N;
            else
                Size = length;
            nodes = new T[Size+1];
            for(ulong x= 1; x<(length+1); x++)
            {
                nodes[x] = A[x-1];
            }
        }

        /// <summary>
        /// parent(uint)
        ///         Method that determines the the parent of any node in the heap.
        /// </summary>
        /// <param name="index">Index of the node for which to extract its parent.</param>
        /// <returns>Index of the parent node given by the input.</returns>
        private uint parent(uint index) { return (index >> 1); }

        /// <summary>
        /// left(uint)
        ///         Method that determines the the left child of any node in the heap.
        /// </summary>
        /// <param name="index">Index of the node for which to extract its left child.</param>
        /// <returns>Index of the left child's node given by the input.</returns>
        private uint left(uint index) { return (index << 1); }

        /// <summary>
        /// right(uint)
        ///         Method that determines the the right child of any node in the heap.
        /// </summary>
        /// <param name="index">Index of the node for which to extract its right child.</param>
        /// <returns>Index of the right child's node given by the input.</returns>
        private uint right(uint index) { return ((index << 1) + 1); }

        /// <summary>
        /// swap(uint, uint)
        ///         Method that swap the data in the heap array for two (2) gived indices.
        /// </summary>
        /// <param name="X">Index of one of the first object to swap in the heap array.</param>
        /// <param name="Y">Index of one of the second object to swap in the heap array.</param>
        private void swap(uint X, uint Y)
        {
            T swapspace = nodes[X];
            nodes[X] = nodes[Y];
            nodes[Y] = swapspace;
           // Console.WriteLine("Swapped: A[" + X + "] with A[" +Y+"]");
           // print();
        }

        /// <summary>
        /// minHeapify(uint)
        ///         Method that maintains the heap property on any given node.
        /// </summary>
        /// <param name="index">Node in which to maintain the min-heap property.</param>
        public void minHeapify(uint index)
        {
            uint parentnode, leftchild, rightchild, smallest = index;
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
        /// buildMinHeap(uint)
        ///         Method used for maintaining the heap property of any given node. Given node i, it 
        ///         will verify that the heap property is maintained and will recursively call on itself 
        ///         from the top-down. Uses minHeapify(ulong) to maintain the heap property.
        /// </summary>
        /// <param name="i"></param>
        private void buildMinHeap(uint i)
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

        /// <summary>
        /// insertElement()
        ///         Method used to insert an element into the heap. The method heapifies the 
        ///         parent node in order to maintain the min heap property.
        /// </summary>
        /// <param name="newelement">New element to be added to the heap.</param>
        public void insertElement(T newelement)
        {
            if (length == Size)
            {
                // Need to resize the heap
                resizeHeap(2 * Size);
                nodes[++length] = newelement;
                minHeapify(parent(length));
            }
            else
            {
                nodes[++length] = newelement;
                minHeapify(parent(length));
            }
                
        }

        /// <summary>
        /// resizeHeap(uint)
        ///         Method to resize the heap array to a new size. The method creates a new 
        ///         array and copies the elements of the old array into it. The new object 
        ///         is then used to replace the old reference.
        ///         
        ///         Method is private because it does not maintain the Min Heap property and 
        ///         could be used to distabilized the heap.
        /// </summary>
        /// <param name="newSize">New size for the heap's array.</param>
        private void resizeHeap(uint newSize)
        {
            // Case 1: New array size is larger than current array size.
            if (newSize > Size)
            {
                T[] newNodes = new T[newSize];
                for (var i = 0; i <= Size; i++)
                {
                    newNodes[i] = nodes[i];
                }
                nodes = newNodes;
                Size = newSize;
            }
            // Case 2: New array is shorter than current array size.
            else if (newSize < Size)
            {
                length = newSize;
            }
            // Case 3: New array size is the same size as current array size.
            // Nothing to do.
        }

        /// <summary>
        /// union(heap)
        ///         Method for the union of two(2) heaps. If the current heap does not 
        ///         have enough room to hold the second heap, the current heap is rezied
        ///         in order for both heaps to be allocated into the current heap. The data 
        ///         from the second heap is the copied to the current heap, buildMinHeap is
        ///         called to maintain the min heap property.
        /// </summary>
        /// <param name="H">Heap object to merge with current heap.</param>
        public void union(heap<T> H)
        {
            if(Size < (length+H.length))
                resizeHeap(2 * (length + H.length));

            for (uint i = 1; i <= (H.length); i++)
            {
                nodes[i + length] = H.nodes[i];
            }
            length = (length + H.length);
            buildMinHeap();

        }
        /// <summary>
        /// union(T[])            
        ///         Method for the union of a heap with an array of the same data type. If the 
        ///         current heap does not have enough room to hold the data from the array, the 
        ///         current heap is rezied in order for data to be allocated into the current 
        ///         heap. The data from the array is then copied to the current heap, buildMinHeap 
        ///         is called to maintain the min heap property.
        /// </summary>
        /// <param name="A">Array with data to merge with current heap.</param>
        public void union(T[] A)
        {
            uint Alength = (uint)A.Length;
            if (Size < (length + Alength))
                resizeHeap(2 * (length + Alength));

            for (uint i = 1; i <= (Alength); i++)
            {
                nodes[i + length] = A[i];
            }
            length = (length + Alength);
            buildMinHeap();
        }
    }
}
