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
            BinomialNode<T> a, b, c;
            a = H1.head();
            b = H2.head();
            H1.head((Min_Degree(H1, H2)).head());
            // Check in case they are both empty
            if (H1.head().Equals(NIL))
            {
                H1.head(NIL);
                return new BinomialHeap<T>(H1);
            }
            // If H2 has a lesser degree than <i>a</i>, then swap them.
            if (H1.head().Equals(b))
                b = a;
            a = H1.head();
            while (!b.Equals( NIL))
            {
                // If <i>a</i> has no sibblings then make <i>b</i> its sibbling and we are done.
                if (a.sibling==NIL)
                {
                    a.sibling = b;
                    H1.head(a);
                    return new BinomialHeap<T>(H1);
                }
                else if (a.sibling.getDegree() < b.getDegree())
                    a = a.sibling;
                else
                {
                    c = b.sibling;
                    b.sibling = a.sibling;
                    a.sibling = b;
                    a = a.sibling;
                    b = c;
                }
            }
            H1.head(a);
            return new BinomialHeap<T>(H1);
        }

        BinomialHeap<T> Binomial_HeapUnion(BinomialHeap<T> H1, BinomialHeap<T> H2)
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
                        //          - move forward to the next tree.
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
                            //          - link prev_x with x, move x and next_x to the next respective tree
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


        /*

BinomialHeap<T> Binomial_HeapUnion(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            BinomialHeap<T> H;
            BinomialNode<T> x, prev_x, next_x;
            x       = new BinomialNode<T> ();
            prev_x  = new BinomialNode<T> ();
            next_x  = new BinomialNode<T> ();
            H = Binomial_HeapMerge(H1, H2);
            if (H.head().Equals(NIL))
                return H;
            prev_x = NIL;
            x = H.head();
            next_x = x.sibling;
            while (next_x != null && !next_x.Equals(NIL))
            {
                if ((x.getDegree() != next_x.getDegree()) ||
                  ((next_x.sibling != null) &&
                    (next_x.sibling.getDegree() == x.getDegree())))
                {   // Case 1 and 2
                    prev_x = x;
                    x = next_x;
                }
                else
                {
                    if (x.getKey() <= next_x.getKey())
                    {   // Case 3
                        x.sibling = next_x.sibling;
                        x.link(next_x);
                    }
                    else
                    {   // Case 4
                        if (prev_x==null || prev_x.isNIL())
                            H.head(next_x);
                        else
                            prev_x.sibling = next_x;
                        next_x.link(x);
                    }
                }
            } 
            return H;
        }         

         */


        BinomialHeap<T> Min_Degree(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            if (H1.head().getDegree() <= H2.head().getDegree())
            {
                if (H1.head().Equals(NIL) && H2.head().Equals(NIL))
                    return H1;
                else
                    return H2;
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
    }
}
