using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProfiler
{
    /// <summary>
    /// Binomial Heap node Data Structured based on CLRS 3rd Edition
    ///     Implementation by: Elias V. Beauchamp Rodriguez
    ///                        As a requirement for the course COMP6785
    ///     The Binomial node is used to implement the data structure used for 
    ///     Binomial Heaps. It implements the IComparable interface in order to
    ///     be used inside other generic structures. The implementation consists
    ///     of a linked list of nodes whos children form Binomial trees of a 
    ///     lesser degreee than their parent.
    ///     
    ///     Binomial node data members:
    ///         - key       : The key used to represent each data member.
    ///         - degree    : The order of the Binomial sub tree headed by the node.
    ///         - parent    : The parent of the current node.
    ///         - sibling   : The sibling of any node contains the right child of 
    ///                         its parent node.
    ///         - child     : The child of any node contains its left child.
    ///         - data      : Any information attached to the data structure.
    ///         - NIL       : Null pointer reference.
    ///         
    ///     Binomial node methods:
    ///         - BinomialNode()    : Default empty constructor
    ///         - BinomialNode(T initialValue, int startkey)
    ///                             : default node with initial data and key
    ///         - BinomialNode(BinomialNode<T> node)
    ///                             : Copy constrctor.
    ///         - BinomialNode(int startKey)
    ///                             : Default node with an initial key.
    ///         - getKey()          : Gets the key for the node.
    ///         - getDegree()       : Gets the node's degree.
    ///         - link(BinomialNode<T> y)
    ///                             : Links the current node with a new node <i>y</i>.
    ///         - link(BinomialNode<T> y, BinomialNode<T> z)
    ///                             : Links node <i>y</i> to node <i>z</i>.
    ///         - IComparable.CompareTo(object obj)
    ///                             : Provides basis for comparing and sorting.
    ///         - print()           : Visualization of the node structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BinomialNode<T> : IComparable
                           where T : IComparable
    {
        /// <summary>
        /// key       : The key used to represent each data member.
        /// </summary>
        int key;
        /// <summary>
        /// degree    : The order of the Binomial sub tree headed by the node.
        /// </summary>
        uint degree;
        /// <summary>
        /// data      : Any information attached to the data structure.
        /// </summary>
        T data;
        /// <summary>
        /// parent    : The parent of the current node.
        /// </summary>
        public BinomialNode<T> parent   { get; set; }
        /// <summary>
        /// sibling   : The sibling of any node contains the right child of 
        ///             its parent node.
        /// </summary>
        public BinomialNode<T> sibling  { get; set; }
        /// <summary>
        /// child     : The child of any node contains its left child.
        /// </summary>
        public BinomialNode<T> child    { get; set; }

        /// <summary>
        /// getKey()          : Gets the key for the node.
        /// </summary>
        /// <returns>Integer</returns>
        public int getKey() { return key; }

        /// <summary>
        /// getDegree()       : Gets the node's degree.
        /// </summary>
        /// <returns>uint</returns>
        public uint getDegree() { return degree; }

        public T getData() { return data; }

        /// <summary>
        /// BinomialNode()    : Default empty constructor
        /// </summary>
        public BinomialNode()
        {
            data =default(T);
            key = 0;
            degree = 0;
            parent = null;
            sibling = null;
            child = null;
        }

        /// <summary>
        /// BinomialNode(T initialValue, int startkey)
        ///     Default node with initial data and key
        /// </summary>
        /// <param name="initialValue">Generic data for the node.</param>
        /// <param name="startKey">Initial key for the node.</param>
        public BinomialNode(T initialValue, int startKey)
        {
            data = initialValue;
            key = startKey;
            degree = 0;
            parent = null;
            sibling = null;
            child = null;
        }

        /// <summary>
        /// BinomialNode(int startKey)
        ///     Default node with an initial key.
        /// </summary>
        /// <param name="startKey"></param>
        public BinomialNode(int startKey)
        {
            data = default(T);
            key = startKey;
            degree = 0;
            parent = null;
            sibling = null;
            child = null;
        }

        /// <summary>
        /// BinomialNode(BinomialNode<T> node)
        ///     Copy constrctor.
        /// </summary>
        /// <param name="node">BinomialNode to replicate.</param>
        public BinomialNode(BinomialNode<T> node)
        {
            data = node.data;
            key = node.key;
            parent = node.parent;
            sibling = node.sibling;
            child = node.child;
        }

        /// <summary>
        /// link(BinomialNode<T> y)
        ///     Links the current node with a new node <i>y</i>.
        /// </summary>
        /// <param name="y">BinomialNode to link.</param>
        public void link(BinomialNode<T> y)
        {
            y.parent = this;
            y.sibling = this.child;
            child = y;
            degree++;
        }
        
        /// <summary>
        /// link(BinomialNode<T> y, BinomialNode<T> z)
        ///     Links node <i>y</i> to node <i>z</i>.
        /// </summary>
        /// <param name="y">BinomialNode to link to <i>z</i></param>
        /// <param name="z">BinomialNode <i>y</i> to be linked onto.</param>
        public void link(BinomialNode<T> y, BinomialNode<T> z)
        {
            y.parent = z;
            y.sibling = z.child;
            z.child = y;
            z.degree++;
        }

        
        /// <summary>
        /// IComparable.CompareTo(object obj)
        ///     Provides basis for comparing and sorting.
        /// </summary>
        /// <param name="obj">Object to compare with.</param>
        /// <returns></returns>
        int IComparable.CompareTo(object obj)
        {
            // Implement IComparable CompareTo method - provide default sort order.
            if (obj == null) return 1;
            var other = obj as BinomialNode<T>;
            if (other != null)
            {
                return this.key.CompareTo(other.key);
            }
            else
                throw new ArgumentException("Object is not a BinomialNode<T>.");

        }

        /// <summary>
        /// print()           : Visualization of the node structure.
        /// </summary>
        public void print()
        {
            BinomialNode<T> current = this;
            BinomialNode<T> NIL = new BinomialNode<T>(int.MinValue);

            string output = "";
            while (current != NIL)
            {
                for (int i = 0; i < current.getDegree(); i++)
                {
                    output += " ";
                }
                output += current.getKey().ToString();
                Console.WriteLine(output);
                if (current.child != NIL)
                    current.child.print();
                current = current.sibling;
            }
        }

    }
}
