using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
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

        

        void Binomial_HeapMerge(BinomialHeap<T> H1, BinomialHeap<T> H2)
        {
            BinomialNode<T> a, b, c;
            a = H1.head();
            b = H2.head();
            H1.head((Min_Degree(H1, H2)).head());
            if (H1.head() == NIL)
                return;
            if (H1.head() == b)
                b = a;
            a = H1.head();
            while (b != NIL)
            {
                if (a.sibling == NIL)
                {
                    a.sibling = b;
                    return;
                }
                else if (a.sibling.getDegree() < b.getDegree())
                    a = a.sibling;
                else
                {
                    c = a.sibling;
                    b.sibling = a.sibling;
                    a.sibling = b;
                    b = c;
                }
            }
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
