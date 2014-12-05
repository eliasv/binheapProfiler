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
    ///     A Binomial Heap is a recursive structure that follows the min-heap
    ///     property.
    /// </summary>
    /// <typeparam name="T">Generic data type for the node storage.</typeparam>
    class BinomialHeap<T>   where T : IComparable
    {
        BinomialNode<T> BHeap;
        static int MinusInf = int.MinValue;
        public BinomialNode<T> NIL = null;

        public BinomialHeap()
        {
            BHeap = new BinomialNode<T>(NIL);
        }

        public BinomialHeap(BinomialNode<T> root)
        {
            BHeap = new BinomialNode<T>();
            root.parent = NIL;
            root.sibling = NIL;
            root.child = NIL;
            BHeap=(new BinomialNode<T>(root.getData(), root.getKey()));
        }

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



        public uint getDegree()
        {
            BinomialNode<T> context = new BinomialNode<T>(this.head());
            while(context.sibling!=null && !context.sibling.isNIL())
                context = context.sibling;
            return context.getDegree();
        }

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

        public BinomialNode<T>     head()                                   { return BHeap;  }
        public void                head(BinomialNode<T> context)            { BHeap = context; }
        public                     BinomialHeap(BinomialHeap<T> context)    { this.BHeap = context.BHeap; }

        public void print()
        {
            this.head().print();
        }

        public void HeapInsert(BinomialNode<T> x)
        {
            BinomialHeap<T> H = new BinomialHeap<T>(x);
            this.head(this.Binomial_HeapUnion(this, H).head());
        }

        public void HeapInsert(int[] keys)
        {
            var len = keys.Length;
            for(int i=0; i < len; i++)
            {
                BinomialHeap<T> H = new BinomialHeap<T>(new BinomialNode<T>(keys[i]));
                this.head(this.Binomial_HeapUnion(this, H).head());
            }
            
        }

        public void HeapInsert(int key)
        {
                BinomialHeap<T> H = new BinomialHeap<T>(new BinomialNode<T>(key));
                this.head(this.Binomial_HeapUnion(this, H).head());
        }
    }
}
