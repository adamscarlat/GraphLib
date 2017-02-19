using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibs.GraphExtensions
{
    /// <summary>
    /// A collection that abstracts the underlying data-structure and allows moving some of the redundant 
    /// code of the search algorithms into modular functions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchCollection<T>
    {
        //Add an item to the collection
        void Add(T item);

        //Remove an item from the collection
        T Remove();

        int Count { get;}
    }

    #region ISearchCollection Implementor

    /// <summary>
    /// Wrapper for the Queue data structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueWrapper<T> : ISearchCollection<T>
    {
        private Queue<T> _queue;

        public QueueWrapper(Queue<T> queue)
        {
            _queue = queue;
        }

        /// <summary>
        /// Number of items in the queue
        /// </summary>
        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }

        /// <summary>
        /// Enqueue an item to the queue
        /// </summary>
        /// <param name="item">item to enqueue</param>
        public void Add(T item)
        {
            _queue.Enqueue(item);
        }

        /// <summary>
        /// Dequque an item from the queue
        /// </summary>
        /// <returns>item dequeued</returns>
        public T Remove()
        {
            return _queue.Dequeue();
        }
    }

    /// <summary>
    /// Wrapper for the Stack data structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StackWrapper<T> : ISearchCollection<T>
    {
        private Stack<T> _stack;

        public StackWrapper(Stack<T> stack)
        {
            _stack = stack;
        }

        /// <summary>
        /// number of items in the stack
        /// </summary>
        public int Count
        {
            get
            {
                return _stack.Count;
            }
        }

        /// <summary>
        /// push an item to the stack
        /// </summary>
        /// <param name="item">item to be pushed onto the stack</param>
        public void Add(T item)
        {
            _stack.Push(item);
        }

        /// <summary>
        /// pop an item off the stack
        /// </summary>
        /// <returns>item popped off the stack</returns>
        public T Remove()
        {
            return _stack.Pop();
        }
    }

    #endregion
}
