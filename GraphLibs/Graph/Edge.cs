using System;

namespace GraphLibs
{
    public class Edge<T> : IComparable<Edge<T>>
    {
        public Edge(Vertex<T> vertexOne, Vertex<T> vertexTwo, float weight)
        {
            VertexOne = vertexOne;
            VertexTwo = vertexTwo;

            if (weight < 1)
                throw new ArgumentException("Edge weight cannot be less than 1");

            Weight = weight;
        }

        /// <summary>
        /// Default weight constructor. Sets the weight to 1
        /// </summary>
        /// <param name="vertexOne">Vertex one</param>
        /// <param name="vertexTwo">Vertex two</param>
        public Edge(Vertex<T> vertexOne, Vertex<T> vertexTwo)
            : this(vertexOne, vertexTwo, 1) { }

        public Vertex<T> VertexOne { get; }

        public Vertex<T> VertexTwo { get; }

        public float Weight { get; set; }

        public int CompareTo(Edge<T> other)
        {
            return this.Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", VertexOne.Label, VertexTwo.Label);
        }

        /// <summary>
        /// Hashcode is based on the two vertices the edge is connected to. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (VertexOne.Label + VertexTwo.Label).GetHashCode();
        }

        /// <summary>
        /// Two edges are equal if their vertices are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if ((obj as Edge<T>) == null)
                return false;

            Edge<T> otherEdge = (Edge<T>)obj;

            return otherEdge.VertexOne.Equals(VertexOne) && otherEdge.VertexTwo.Equals(VertexTwo);
        }

    }
}