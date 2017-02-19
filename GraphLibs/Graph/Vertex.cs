using System.Collections.Generic;

namespace GraphLibs
{
    /// <summary>
    /// Represents a graph vertex. The vertex is identified by a unique label. 
    /// This vertex accepts an object of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vertex<T>
    {
        public Vertex(T vertexObject)
        {
            VertexObject = vertexObject;
        }

        public string Label { get; }

        public T VertexObject { get; private set; }

        public override string ToString()
        {
            return VertexObject.ToString();
        }

        public override bool Equals(object obj)
        {
            if ((obj as Vertex<T>) == null)
                return false;

            Vertex<T> otherVertex = (Vertex<T>)obj;

            return otherVertex.VertexObject.Equals(VertexObject);
        }

        public override int GetHashCode()
        {
            return VertexObject.GetHashCode();
        }

    }
}
