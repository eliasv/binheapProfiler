using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class BTNode<T>
    {
        private T key { get; set; }
        private BTNode<T> parent { get; set; }
        private BTNode<T> childLeft { get; set; }
        private BTNode<T> childRight { get; set; }

        public BTNode()
        {
            parent = null;
            childLeft = null;
            childRight = null;
        }

        public BTNode(T item)
            : this()
        {
            key = item;
        }

        public BTNode(BTNode<T> N)
        {
            key = N.key;
            parent = N.parent;
            childLeft = N.childLeft;
            childRight = N.childRight;
        }
    }

    class BinarySearchTree<T>
    {
        BSTNode<T> node;

    }
}
