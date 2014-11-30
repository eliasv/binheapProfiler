using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class BinomialNode<T>
    {
        uint key;
        uint degree;
        T data;
        BinomialNode<T> parent;
        BinomialNode<T> sibling;
        BinomialNode<T> child;
        
        public BinomialNode(T initialValue, uint startKey)
        {
            data = initialValue;
            key = startKey;
            degree = 0;
            parent = null;
            sibling = null;
            child = null;
        }

        public void link(BinomialNode<T> y)
        {
            y.parent = this;
            y.sibling = this.child;
            child = y;
            degree++;
        }
        
        public void link(BinomialNode<T> y, BinomialNode<T> z)
        {
            y.parent = z;
            y.sibling = z.child;
            z.child = y;
            z.degree++;
        }
    }
}
