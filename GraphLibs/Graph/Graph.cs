using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibs
{
    public class Graph<T>
    {
        /// <summary>
        /// Adjecency list to hold vertices and edges
        /// </summary>
        private Dictionary<Vertex<T>, LinkedList<Edge<T>>> _adjMap;
        
        /// <summary>
        /// Is the graph directed
        /// </summary>
        private bool _isDirectedGraph;

        /// <summary>
        /// Number of vertices currently in the graph
        /// </summary>
        public int VertexCount { get; private set; }

        public Graph(bool isDirected = false)
        {
            _adjMap = new Dictionary<Vertex<T>, LinkedList<Edge<T>> >();
            _isDirectedGraph = isDirected;
            VertexCount = 0;
        }

        #region Add Vertices

        /// <summary>
        /// Adds a vertex to the graph
        /// </summary>
        /// <param name="vertex">Vertex to add</param>
        /// <returns>True if vertex was added successfully, false otherwise</returns>
        public bool AddVertex(Vertex<T> vertex)
        {
            if (vertex == null)
                return false;

            if (_adjMap.ContainsKey(vertex))
                return false;

            _adjMap.Add(vertex, new LinkedList<Edge<T>>());
            VertexCount += 1;

            return true;
        }

        /// <summary>
        /// Adds a vertex by its object to the graph
        /// </summary>
        /// <param name="vertexObject">object to be contained in a vertex</param>
        /// <returns>True if vertex was added successfully, false otherwise</returns>
        public bool AddVertex(T vertexObject)
        {
            if (vertexObject == null)
                return false;

            var vertex = new Vertex<T>(vertexObject);
            return AddVertex(vertex);
        }

        /// <summary>
        /// Adds multiple vertices to the graph
        /// </summary>
        /// <param name="vertices">list of vertex objects</param>
        /// <returns>True if all vertices were added successfully, false otherwise</returns>
        public bool AddVertices(IEnumerable<Vertex<T>> vertices)
        {
            if (vertices == null)
                return false;

            bool allAdded = true;
            foreach (var vertex in vertices)
                allAdded &= AddVertex(vertex);

            return allAdded;
        }

        /// <summary>
        /// Adds multiple vertices by their object to the graph
        /// </summary>
        /// <param name="vertices">list of vertex objects</param>
        /// <returns>True if all vertices were added successfully, false otherwise</returns>
        public bool AddVertices(IEnumerable<T> vertexObjects)
        {
            if (vertexObjects == null)
                return false;

            var vertices = new List<Vertex<T>>();
            foreach (var obj in vertexObjects)
                vertices.Add(new Vertex<T>(obj));

            return AddVertices(vertices);

        }

        #endregion

        #region Remove Vertices

        /// <summary>
        /// Removes a vertex and all edges to it from the graph
        /// </summary>
        /// <param name="vertex">Vertex to be removed</param>
        /// <returns>True if the vertex was removed successfully, false otherwise</returns>
        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (vertex == null)
                return false;

            if (!_adjMap.ContainsKey(vertex))
                return false;

            //remove edges connected to the vertex to be removed
            foreach (var edgeList in _adjMap.Values)
            {
                var current = edgeList.First;
                
                while (current != null)
                {
                    var edge = current.Value;
                    var nextEdgeNode = current.Next;
                    if (edge.VertexOne.Equals(vertex) || edge.VertexTwo.Equals(vertex))
                        edgeList.Remove(edge);

                    current = nextEdgeNode;
                }
            }

            _adjMap.Remove(vertex);

            return true;
        }

        /// <summary>
        /// Removes a vertex by its object and all edges to it from the graph
        /// </summary>
        /// <param name="vertex">Vertex to be removed</param>
        /// <returns>True if the vertex was removed successfully, false otherwise</returns>
        public Vertex<T> RemoveVertex(T vertexObject)
        {
            if (vertexObject == null)
                return null;

            var vertex = new Vertex<T>(vertexObject);
            if (RemoveVertex(vertex))
            {
                VertexCount -= 1;
                return vertex;
            }

            return null;
        }

        #endregion

        #region Add Edge

        /// <summary>
        /// Adds an edge between two vertices. If one or both of the vertices is not in the graph it 
        /// will be added to the graph.
        /// </summary>
        /// <param name="vertexOne">Vertex from</param>
        /// <param name="vertexTwo">Vertex To</param>
        /// <param name="weight">Weight of edge</param>
        /// <returns></returns>
        public bool AddEdge(Vertex<T> vertexOne, Vertex<T> vertexTwo, float weight = 1)
        {
            if (vertexOne == null || vertexTwo == null)
                return false;

            if (vertexOne.Equals(vertexTwo))
                return false;

            if (!_adjMap.ContainsKey(vertexOne))
                _adjMap.Add(vertexOne, new LinkedList<Edge<T>>());

            if (!_adjMap.ContainsKey(vertexTwo))
                _adjMap.Add(vertexTwo, new LinkedList<Edge<T>>());

            if (EdgeExists(vertexOne, vertexTwo))
                return false;

            var edge = new Edge<T>(vertexOne, vertexTwo, weight);

            _adjMap[vertexOne].AddLast(edge);

            //if the graph is directed, do not produce an opposite edge
            if (_isDirectedGraph)
                return true;

            var oppositeEdge = new Edge<T>(vertexTwo, vertexOne, weight);

            _adjMap[vertexTwo].AddLast(oppositeEdge);
            return true;
        }

        /// <summary>
        /// Checks if an edge already exists between vertex one and vertex two.
        /// </summary>
        /// <param name="vertexOne">Vertex from</param>
        /// <param name="vertexTwo">Vertex to</param>
        /// <returns>True if an edge exists, false otherwise</returns>
        private bool EdgeExists(Vertex<T> vertexOne, Vertex<T> vertexTwo)
        {
            var vertexOneList = _adjMap[vertexOne];
            foreach(var edge in vertexOneList)
            {
                if (edge.VertexTwo.Equals(vertexTwo))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an edge between two vertices by their objects. If one or both of the vertices is not in the graph it 
        /// will be added to the graph.        
        /// </summary>
        /// <param name="vertexObjectOne">Object of vertex one</param>
        /// <param name="vertexObjectTwo">Object of vertex two</param>
        /// <param name="weight">Weight of the edge added</param>
        /// <returns></returns>
        public bool AddEdge(T vertexObjectOne, T vertexObjectTwo, float weight = 1)
        {
            if (vertexObjectOne == null || vertexObjectTwo == null)
                return false;

            return AddEdge(new Vertex<T>(vertexObjectOne), new Vertex<T>(vertexObjectTwo), weight);
        }


        #endregion

        /// <summary>
        /// Gets all the neighbors of a particular vertex
        /// </summary>
        /// <param name="vertex">subject vertex</param>
        /// <returns>An enumerable of all neighbors of subject vector</returns>
        public IEnumerable<Vertex<T>> GetAllNeighborsOf(Vertex<T> vertex)
        {
            if (vertex == null || !_adjMap.ContainsKey(vertex))
                return null;

            var edgeList = _adjMap[vertex];

            if (edgeList == null)
                return null;

            var neighbors = new List<Vertex<T>>();
            foreach (var edge in edgeList)
                neighbors.Add(edge.VertexTwo);

            return neighbors;
        }

        public IEnumerable<T> GetAllNeighborsOf(T vertexObject)
        {
            if (vertexObject == null)
                return null;

            var vertex = new Vertex<T>(vertexObject);
            var neighbors = GetAllNeighborsOf(vertex) ?? Enumerable.Empty<Vertex<T>>();

            var neighborsObjects = new List<T>();
            foreach (var neighborVertex in neighbors)
                neighborsObjects.Add(neighborVertex.VertexObject);

            return neighborsObjects;
        }

        public void PrintGraph()
        {
            foreach(var vertex in _adjMap.Keys)
            {
                Console.Write("Vertex {0} neighbors: ", vertex);
                foreach(var edge in _adjMap[vertex])
                {
                    Console.Write("{0}, ", edge.VertexTwo);
                }
                Console.WriteLine(" ");
            }
            
        }

    }
}
