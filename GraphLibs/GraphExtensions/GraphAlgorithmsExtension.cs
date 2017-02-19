using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibs.GraphExtensions
{
    public static class GraphAlgorithmsExtension
    {

        /// <summary>
        /// Common code to search algorithms
        /// </summary>
        /// <typeparam name="T">Type of objects in the graph vertices</typeparam>
        /// <param name="graph">Graph structure to operate on</param>
        /// <param name="collection">Abstraction of the collection the search algorithm uses (e.g. for DFS collection will be a stack)</param>
        /// <param name="searchItem">Item to find</param>
        /// <returns>True if search item is in the graph, false otherwise</returns>
        private static bool SearchHelper<T>(Graph<T> graph, ISearchCollection<T> collection, T searchItem)
        {
            var visited = new HashSet<T>();

            while (collection.Count != 0)
            {
                var currentNode = collection.Remove();
                Console.WriteLine("currentNode: {0}. Neighbors:  ", currentNode);

                if (currentNode.Equals(searchItem))
                    return true;

                var neighbors = graph.GetAllNeighborsOf(currentNode)?.ToList();
                if (neighbors == null)
                    continue;

                foreach (var node in neighbors)
                {
                    if (!visited.Contains(node))
                    {
                        Console.WriteLine(node);
                        collection.Add(node);
                        visited.Add(node);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Breadth first search to find if an element exsists in the graph
        /// </summary>
        /// <typeparam name="T">Vertex Object Type</typeparam>
        /// <param name="graph">Graph datastructure</param>
        /// <param name="startNode">Node to start the search from</param>
        /// <param name="searchItem">Item to be searches</param>
        /// <returns>True if item exists in the graph, false otherwise</returns>
        public static bool BreadthFirstSearch<T>(this Graph<T> graph, T startNode, T searchItem)
        {
            ISearchCollection<T> collection = new QueueWrapper<T>(new Queue<T>());
            collection.Add(startNode);

            return SearchHelper<T>(graph, collection, searchItem);
        }

        /// <summary>
        /// Depth first search to find if an element exsists in the graph
        /// </summary>
        /// <typeparam name="T">Vertex Object Type</typeparam>
        /// <param name="graph">Graph datastructure</param>
        /// <param name="startNode">Node to start the search from</param>
        /// <param name="searchItem">Item to be searches</param>
        /// <returns>True if item exists in the graph, false otherwise</returns>
        public static bool DepthFirstSearch<T>(this Graph<T> graph, T startNode, T searchItem)
        {
            ISearchCollection<T> collection = new StackWrapper<T>(new Stack<T>());
            collection.Add(startNode);

            return SearchHelper<T>(graph, collection, searchItem);            
        }

        //TODO: tests - large graphs
        public static bool ParallelBreadthFirstSearch<T>(this Graph<T> graph, T startNode, T searchItem)
        {
            var visited = new ConcurrentDictionary<T, bool>();
            var queue = new ConcurrentQueue<T>();
            bool isFound = false;
            object isFoundLock = new object();

            queue.Enqueue(startNode);

            while (!isFound && queue.Count != 0)
            {
                var taskList = new List<Task>();
                foreach (var node in queue)
                {
                    taskList.Add(Task.Factory.StartNew(() =>
                    {
                        T currentNode;
                        if (queue.TryDequeue(out currentNode))
                        {
                            Console.WriteLine("Thread-id: {0}. currentNode: {1}. Neighbors:  ", System.Threading.Thread.CurrentThread.ManagedThreadId, currentNode);
                            if (currentNode.Equals(searchItem))
                            {
                                lock (isFoundLock) isFound = true;
                                return;
                            }

                            var neighbors = graph.GetAllNeighborsOf(currentNode);
                            foreach (var neighbor in neighbors)
                            {
                                if (!visited.ContainsKey(neighbor))
                                {
                                    Console.WriteLine("Thread-id: {0}. {1}:  ", System.Threading.Thread.CurrentThread.ManagedThreadId, neighbor);
                                    visited.TryAdd(neighbor, true);
                                    queue.Enqueue(neighbor);
                                }
                            }
                        }
                    }));
                }
                Task.WaitAll(taskList.ToArray());
            }
            return isFound;
        }
    }
}
