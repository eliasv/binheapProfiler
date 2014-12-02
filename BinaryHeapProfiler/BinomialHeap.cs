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
    /// <typeparam name="T">Generic data type for the nodes.</typeparam>
    class BinomialHeap<T>   where T : IComparable
    {
        heap<BinomialNode<T>> BHeap;
        static int MinusInf = int.MinValue;
        BinomialNode<T> NIL = new BinomialNode<T>(MinusInf);

        public BinomialHeap()
        {
            BHeap = new heap<BinomialNode<T>>(10);
            BHeap.insertElement(NIL);
        }

        

        BinomialHeap<T> Binomial_HeapMerge(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            BinomialNode<T> a, b, c;
            a = H1.head();
            b = H2.head();
            H1.head((Min_Degree(H1, H2)).head());
            // Check in case they are both empty
            if (H1.head() == NIL)
            {
                H1.head(NIL);
                return new BinomialHeap<T>(H1);
            }
            // If H2 has a lesser degree than <i>a</i>, then swap them.
            if (H1.head() == b)
                b = a;
            a = H1.head();
            while (b != NIL)
            {
                // If <i>a</i> has no sibblings then make <i>b</i> its sibbling and we are done.
                if (a.sibling == NIL)
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
            return new BinomialHeap<T>(H1);
        }



        BinomialHeap<T> Min_Degree(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            if (H1.head().getDegree() <= H2.head().getDegree())
                return H1;
            else
                return H2;
        }

        public BinomialNode<T>     head()                                   { return BHeap.nodes[0];  }
        public void                head(BinomialNode<T> context)            { BHeap.nodes[0] = context; }
        public                     BinomialHeap(BinomialHeap<T> context)    { this.BHeap = context.BHeap; }

        int calculateDegree(heap<T> BinHeap)
        {
            return (int)Math.Floor(Math.Log((double)BinHeap.length));
        }


    }
}
