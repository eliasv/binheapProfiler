using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class BinomialNode<T> : IComparable
                           where T : IComparable
    {
        int key;
        uint degree;
        T data;
        public BinomialNode<T> parent   { get; set; }
        public BinomialNode<T> sibling  { get; set; }
        public BinomialNode<T> child    { get; set; }

        public int getKey() { return key; }

        public uint getDegree() { return degree; }

        public BinomialNode(T initialValue, int startKey)
        {
            data = initialValue;
            key = startKey;
            degree = 0;
            parent = null;
            sibling = null;
            child = null;
        }

        public BinomialNode(int startKey)
        {
            data = default(T);
            key = startKey;
            degree = 0;
            parent = null;
            sibling = null;
            child = null;
        }

        public BinomialNode(BinomialNode<T> node)
        {
            data = node.data;
            key = node.key;
            parent = node.parent;
            sibling = node.sibling;
            child = node.child;
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

        // Implement IComparable CompareTo method - provide default sort order.
        int IComparable.CompareTo(object obj)
        {
            if (obj == null) return 1;
            var other = obj as BinomialNode<T>;
            if (other != null)
            {
                return this.key.CompareTo(other.key);
            }
            else
                throw new ArgumentException("Object is not a BinomialNode<T>.");

        }
    }
}
