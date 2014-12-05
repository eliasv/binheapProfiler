using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    /// <summary>
    /// Binomial Heap Data Structured based on CLRS 2nd Edition
    ///     Implementation by: Elias V. Beauchamp Rodriguez
    ///                        As a requirement for the course COMP6785
    ///     A Binomial Heap is a recursive tree structure that follows the min-heap
    ///     property.
    ///     
    ///     BinomialHeap Data Memeber:
    ///         - BHeap     : BinomialNode that contains the data structure to be maintained by the 
    ///                         BinomialHeap class. Reffered as the head of the Binomial heap.
    ///         - NIL       : Null reference used to find leafs in the trees.
    ///     
    ///     BinomialHeap Methods:
    ///         - BinomialHeap()                : Default constructor for the class, it returns an 
    ///                                           empty data structure.
    ///         - BinomialHeap(BinomialNode)    : Constructor used for the creation of Binomial Heaps
    ///                                           from a BinomialNode structure. Used primerally as a
    ///                                           way to generate new objects as return values.
    ///         - BinomialHeap(BinomialHeap)    : Copy constructor. Used in the same manner as 
    ///                                           BinomialNode constructor.
    ///         - head()                        : Returns the first node of the data structure. In a 
    ///                                           Binomial Heap that is the element with the smallest
    ///                                           Degree.
    ///         - head(BinomialNode)            : Replaces the current head of the Binomial Heap with
    ///                                           a new node.
    ///         - Binomial_HeapMerge(BinomialHeap, BinomialHeap)
    ///                                         : Merges two (2) Binomial Heaps into a single link-list 
    ///                                           like structure as a first step in the union of two 
    ///                                           Binomial Heaps.
    ///         - Binomial_HeapUnion(BinomialHeap, BinomialHeap)
    ///                                         : Performs the Union operation on two (2) Binomial Heaps,
    ///                                           the process requires a HeapMerge in order to then verify 
    ///                                           the correctness of the new structure by continually linking
    ///                                           BinomialNodes of the same degree to form trees of a higher
    ///                                           degree. The process repeats until there is, at most, a 
    ///                                           single node with a particular degree.
    ///         - getDegree()                   : Traverses a Binomial Heap to find the structure with the 
    ///                                           maximum degree.
    ///         - Min_Degree(BinomialHeap, BinomialHeap)
    ///                                         : Compares two (2) Binomial Heaps and returns the Binomial Heap
    ///                                           with the smaller degree.
    ///         - HeapInsert(BinomialNode)      : Inserts a BinomialNode into a Binomial Heap. Uses the Union operation.
    ///         - HeapInsert(int[])             : Iteratively inserts an array of keys into a Binomial Heap by 
    ///                                           means of the HeapInsert(BinomialNode) method. Used for profiling.
    ///         - HeapInsert(int)               : Inserts a single key into a Binomial Heap. Used for profiling.
    ///         - print()                       : Visualization of the Binomial Heap for debugging purposes.
    ///                                           Note: Currently in a pre-alpha implementation.
    /// </summary>
    /// <typeparam name="T">Generic data type for node storage.</typeparam>
    class BinomialHeap<T>   where T : IComparable
    {
        /// <summary>
        /// BinomialNode that contains the data structure to be maintained by the BinomialHeap class. 
        /// Reffered as the head of the Binomial heap.
        /// </summary>
        BinomialNode<T> BHeap;

        /// <summary>
        /// Null reference used to find leafs in the trees.
        /// </summary>
        public BinomialNode<T> NIL = null;

        /// <summary>
        /// BinomialHeap()
        /// 
        /// Default constructor for the class, it returns an empty data structure.
        /// </summary>
        public BinomialHeap()
        {
            BHeap = new BinomialNode<T>(NIL);
        }

        /// <summary>
        /// BinomialHeap(BinomialNode)
        /// Constructor used for the creation of Binomial Heaps from a BinomialNode 
        /// structure. Used primerally as a way to generate new objects as return values.
        /// </summary>
        /// <param name="root">Node to be used as the head of the Binomial Heap.</param>
        public BinomialHeap(BinomialNode<T> root)
        {
            BHeap = new BinomialNode<T>();
            root.parent = NIL;
            root.sibling = NIL;
            root.child = NIL;
            BHeap=(new BinomialNode<T>(root.getData(), root.getKey()));
        }

        /// <summary>
        /// BinomialHeap(BinomialHeap)
        /// 
        /// Copy constructor. Used in the same manner as BinomialNode constructor.
        /// </summary>
        /// <param name="context">Object to replicate.</param>
        public BinomialHeap(BinomialHeap<T> context) { this.BHeap = context.BHeap; }

        /// <summary>
        /// head()
        /// 
        /// Returns the first node of the data structure. In a Binomial Heap that is the 
        /// element with the smallest Degree.
        /// </summary>
        /// <returns>BinonialNode that contais the head of the Binomial Heap.</returns>
        public BinomialNode<T> head() { return BHeap; }

        /// <summary>
        /// head(BinomialNode)
        /// 
        /// Replaces the current head of the Binomial Heap with a new node.
        /// </summary>
        /// <param name="context">BinomialNode with the new head of the Binomial Heap.</param>
        public void head(BinomialNode<T> context) { BHeap = context; }

        /// <summary>
        /// Binomial_HeapMerge(BinomialHeap, BinomialHeap)
        /// 
        /// Merges two (2) Binomial Heaps into a single link-list like structure as a first step in the 
        /// union of two Binomial Heaps.
        /// </summary>
        /// <param name="H1">Binomial Heap to merge.</param>
        /// <param name="H2">Binomial Heap to merge.</param>
        /// <returns>Returns a new Binomial Heap that contains a linked list with the concatenation of H1 and H2.</returns>
        BinomialHeap<T> Binomial_HeapMerge(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            // Merge the two Binomial Heaps and prepare them for the UNION operation
            BinomialHeap<T> H = new BinomialHeap<T>();
            BinomialNode<T> BN1, BN2, tmp;
            H = Min_Degree(H1, H2);
            if(H.Equals(H1))
            {
                BN1 = H1.head();
                BN2 = H2.head();
            }
            else
            {
                BN2 = H1.head();
                BN1 = H2.head();
            }
            while(BN2!=null && !BN2.isNIL())
            {
                if(BN1.sibling == null || BN1.sibling.isNIL())
                {
                    // Done with the operation and just add the rest 
                    // of the structure to the sibling of BN1
                    BN1.sibling = BN2;
                    return new BinomialHeap<T>(H);
                }
                else if ( (BN2.sibling==null) || (BN1.sibling.getDegree() < BN2.sibling.getDegree()))
                {
                    // Need to sort by increasing order of degree to maintain the minHeap property
                    // therefore we just move to the next node
                    BN1 = BN1.sibling;
                }
                else
                {
                    // Insert a node from BN2 into the list BN1
                    tmp = BN2.sibling;
                    BN2.sibling = BN1.sibling;
                    BN1.sibling = BN2;
                    BN2 = tmp;
                }
            }
            H.head(BN1);
            return new BinomialHeap<T>(H);
        }

        /// <summary>
        /// Binomial_HeapUnion(BinomialHeap, BinomialHeap)
        /// 
        /// Performs the Union operation on two (2) Binomial Heaps, the process requires a HeapMerge in order to then 
        /// verify the correctness of the new structure by continually linking BinomialNodes of the same degree to 
        /// form trees of a higher degree. The process repeats until there is, at most, a single node with a particular 
        /// degree.
        /// </summary>
        /// <param name="H1">Binomial Heap to perform Union with.</param>
        /// <param name="H2">Binomial Heap to perform Union with.</param>
        /// <returns>Binomial Heap with the Union of H1 and H2.</returns>
        public BinomialHeap<T> Binomial_HeapUnion(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            BinomialHeap<T> H;
            BinomialNode<T> x, prev_x, next_x;
            x       = new BinomialNode<T> ();
            prev_x  = new BinomialNode<T> (int.MinValue);
            next_x  = new BinomialNode<T> ();
            H = Binomial_HeapMerge(H1, H2);
            if (H.head().Equals(NIL))
                return H;
            x = H.head();
            prev_x.sibling = x;
            next_x = x.sibling;
            while(next_x != null && !next_x.isNIL())
            {
                // Three (3) possible cases:
                // Case 1: prev_x.degree < x.degree OR x == null
                if (!prev_x.isNIL() && (prev_x.getDegree() < x.getDegree()))
                {
                    //          - subtrees have correct order, nothing to do
                    //          - move forward to the next tree.
                    prev_x = x;
                    x = next_x;
                    next_x = next_x.sibling;
                }
                // Case 2: prev_x.degree == x.degree == next_x.degree
                else
                {
                    if (prev_x.getDegree() == x.getDegree() && x.getDegree() == next_x.getDegree() && !prev_x.isNIL())
                    //          - same as Case 1
                    {
                        //          - need to link x and next_x
                        //          - move forward to induce either case 3 or case 4
                        prev_x = x;
                        x = next_x;
                        next_x = next_x.sibling;
                    }
                    else
                    {
                        // Case 3: prev_x.degree == x.degree AND (x.degree < next_x.degree OR next_x = null)
                        if (x.getDegree() == next_x.getDegree() &&
                           (next_x.isNIL() ||
                           (next_x != null &&
                           (x.getKey() < next_x.getKey()))))
                        {
                            //          - link next_x with x, move x and next_x to the next respective tree
                            x.sibling = next_x.sibling;
                            x.link(next_x);
                            next_x = x.sibling;
                        }
                        // Case 4: prev_x.degree == x.degree AND (x.degree > next_x.degree OR next_x = null)
                        else if (x.getDegree() == next_x.getDegree() &&
                           (next_x.isNIL() ||
                           (next_x != null &&
                           (x.getKey() > next_x.getKey()))))
                        {
                            // reverse the link order to preserve minHeap property
                            if (prev_x == null || prev_x.isNIL())
                                H.head(next_x);
                            else
                                prev_x.sibling = next_x;
                            next_x.link(x);
                            x = next_x;
                            next_x = x.sibling;
                        }
                        else
                        {
                            prev_x = x;
                            x = next_x;
                            next_x = next_x.sibling;
                        }
                    }
                }
            }
            return H;
        }

        /// <summary>
        /// getDegree()
        /// 
        /// Traverses a Binomial Heap to find the structure with the maximum degree.
        /// </summary>
        /// <returns>uint with the degree of the Binomial Heap.</returns>
        public uint getDegree()
        {
            BinomialNode<T> context = new BinomialNode<T>(this.head());
            while(context.sibling!=null && !context.sibling.isNIL())
                context = context.sibling;
            return context.getDegree();
        }

        /// <summary>
        /// Min_Degree(BinomialHeap, BinomialHeap)
        /// 
        /// Compares two (2) Binomial Heaps and returns the Binomial Heap with the smaller degree.
        /// </summary>
        /// <param name="H1">Binomial Heap</param>
        /// <param name="H2">Binomial Heap</param>
        /// <returns>Binomial Heap with tha has the lesser degeree betwwen H1 and H2.</returns>
        BinomialHeap<T> Min_Degree(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            if (H1.head().getDegree() < H2.head().getDegree())
            {
                if (!H1.head().Equals(NIL))
                    return H1;
                else
                    return new BinomialHeap<T>();
            }
            else
                return H2;
        }

        /// <summary>
        /// print()
        /// 
        /// Visualization of the Binomial Heap for debugging purposes. 
        /// 
        /// Note: Currently in a pre-alpha implementation.
        /// </summary>
        public void print()
        {
            this.head().print();
        }

        /// <summary>
        /// HeapInsert(BinomialNode)
        /// 
        /// Inserts a BinomialNode into a Binomial Heap. Uses the Union operation.
        /// </summary>
        /// <param name="x">BinomialNode to insert into the Binomial Heap.</param>
        public void HeapInsert(BinomialNode<T> x)
        {
            BinomialHeap<T> H = new BinomialHeap<T>(x);
            this.head(this.Binomial_HeapUnion(this, H).head());
        }

        /// <summary>
        /// HeapInsert(int[])
        /// 
        /// Iteratively inserts an array of keys into a Binomial Heap by means of the 
        /// HeapInsert(BinomialNode) method. Used for profiling.
        /// </summary>
        /// <param name="keys">int array with a set of keys to add into a Binomial Heap.</param>
        public void HeapInsert(int[] keys)
        {
            var len = keys.Length;
            for(int i=0; i < len; i++)
            {
                this.HeapInsert((new BinomialNode<T>(keys[i])));
            }
            
        }

        /// <summary>
        /// HeapInsert(int[], uint, uint)
        /// 
        /// Iteratively inserts a sub array of keys into a Binomial Heap by means of the 
        /// HeapInsert(BinomialNode) method. Used for profiling.
        /// </summary>
        /// <param name="keys">int array with a set of keys to add into a Binomial Heap.</param>
        /// <param name="start">First index of the sub array.</param>
        /// <param name="end">last index of the sub array.</param>
        public void HeapInsert(uint[] keys, uint start, uint end)
        {

            var len = keys.Length;
            if (len < end)
                throw new IndexOutOfRangeException("Array is shorter than the final sub array element.");
            for (ulong x = 1; x < (end); x++)
            {
                this.HeapInsert((new BinomialNode<T>((int)keys[x-1])));
            }
        }

        /// <summary>
        /// HeapInsert(int)
        /// 
        /// Inserts a single key into a Binomial Heap. Used for profiling.
        /// </summary>
        /// <param name="key">Key to insert into the Binomial Heap.</param>
        public void HeapInsert(int key)
        {
                BinomialHeap<T> H = new BinomialHeap<T>(new BinomialNode<T>(key));
                this.head(this.Binomial_HeapUnion(this, H).head());
        }


    }
}
