using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GraphLibs;
using System.Linq;

namespace GraphLibUnitTests
{
    [TestClass]
    public class AddRemoveVerticesEdgesTests
    {
        #region Add Vertices Tests

        [TestMethod]
        public void AddVertex_TestByCount()
        {
            //Arrange
            var vertices = CreateTestVertices();
            var graph = new Graph<char>();

            //Act
            foreach (var v in vertices)
                graph.AddVertex(v);

            //Assert
            Assert.AreEqual(vertices.Count, graph.VertexCount);

        }

        [TestMethod]
        public void AddVertexByObject_TestByCount()
        {
            //Arrange
            char vertexObj = 'A';
            var graph = new Graph<char>();

            //Act
            graph.AddVertex(vertexObj);

            //Assert
            Assert.AreEqual(1, graph.VertexCount);
        }

        [TestMethod]
        public void AddVertices_TestByCountAndBoolean()
        {
            //Arrange
            var vertices = CreateTestVertices();
            var graph = new Graph<char>();

            //Act
            bool allAdded = graph.AddVertices(vertices);

            //Assert
            Assert.AreEqual(vertices.Count, graph.VertexCount);
            Assert.IsTrue(allAdded);
        }

        [TestMethod]
        public void AddNullVertex_TestByCount()
        {
            //Arrange
            var vertices = CreateTestVertices();
            vertices[0] = null;
            var graph = new Graph<char>();

            //Act
            graph.AddVertices(vertices);

            //Assert
            Assert.AreEqual(vertices.Count - 1, graph.VertexCount);
        }

        [TestMethod]
        public void AddNullVertices_TestByCountAndBoolean()
        {
            //Arrange
            var vertices = CreateTestVertices();
            vertices[0] = null;
            var graph = new Graph<char>();

            //Act
            var allAdded = graph.AddVertices(vertices);

            //Assert
            Assert.AreEqual(vertices.Count - 1, graph.VertexCount);
            Assert.IsFalse(allAdded);
        }

        [TestMethod]
        public void AddSameVertex_TestByCountAndBoolean()
        {
            //Arrange
            var vertex = new Vertex<char>('A');
            var graph = new Graph<char>();

            bool isAdded;

            //Act
            isAdded = graph.AddVertex(vertex);
            isAdded = graph.AddVertex(vertex);

            //Assert
            Assert.AreEqual(1, graph.VertexCount);
            Assert.IsFalse(isAdded);
        }

        [TestMethod]
        public void AddTwoVerticesWithSameID_TestByCountAndBoolean()
        {
            //Arrange
            var vertex1 = new Vertex<char>('A');
            var vertex2 = new Vertex<char>('A');
            var graph = new Graph<char>();

            bool isAdded;

            //Act
            isAdded = graph.AddVertex(vertex1);
            isAdded = graph.AddVertex(vertex2);

            //Assert
            Assert.AreEqual(1, graph.VertexCount);
            Assert.IsFalse(isAdded);
        }

        #endregion

        #region Remove Vertices Tests

        [TestMethod]
        public void RemoveVertex_TestByCount()
        {
            //Arrange
            var vertices = CreateTestVertices();
            var graph = new Graph<char>();

            //Act
            foreach (var v in vertices)
                graph.AddVertex(v);

            graph.RemoveVertex('A');

            //Assert
            Assert.AreEqual(vertices.Count - 1, graph.VertexCount);
        }

        [TestMethod]
        public void RemoveConnectedVertex_TestByCountAndNeighbors()
        {
            //Arrange
            var vertexStart = new Vertex<char>('0');
            var vertices = CreateTestVertices();
            var graph = new Graph<char>(isDirected: true);

            graph.AddVertex(vertexStart);

            foreach (var v in vertices)
                graph.AddVertex(v);

            foreach (var vertex in vertices)
                graph.AddEdge(vertexStart, vertex);


            var neighborsOfVertexStart = graph.GetAllNeighborsOf(vertexStart);
            foreach(var vertex in vertices)
            {
                Assert.IsTrue(neighborsOfVertexStart.Contains(vertex), string.Format("vertex {0} is not in the neighbor list of vertexStart", vertex));
            }

            //Act
            graph.RemoveVertex(vertexStart);

            //Assert
            //check opposite edge - non directed graph
            foreach (var vertex in vertices)
            {
                var neighbors = graph.GetAllNeighborsOf(vertex);
                Assert.IsFalse(neighbors.Contains(vertexStart), "Vertex is still in neighbors list");
            }



        }

        [TestMethod]
        public void RemoveVertexInACompleteGraph_TestByCount()
        {
            var vertices = CreateTestVertices();
            var graph = new Graph<char>();

            foreach (var v1 in vertices)
            {
                foreach (var v2 in vertices)
                {
                    if (!v1.Equals(v2))
                        graph.AddEdge(v1, v2);
                }
            }

            var vertexD = graph.RemoveVertex('D');
            vertices.Remove(vertexD);

            foreach (var vertex in vertices)
            {
                var neighbors = graph.GetAllNeighborsOf(vertex);
                Assert.IsFalse(neighbors.Contains(vertexD), "Vertex is still in neighbors list");
            }
        }

        #endregion

        #region Add Edges Tests

        [TestMethod]
        public void AddEdgeFromOneToAll_Test()
        {
            //Arrange
            var vertexStart = new Vertex<char>('0');
            var vertices = CreateTestVertices();
            var graph = new Graph<char>();

            graph.AddVertex(vertexStart);

            foreach (var v in vertices)
                graph.AddVertex(v);

            //Act
            bool isAllAdded = true;
            foreach (var vertex in vertices)
                isAllAdded &= graph.AddEdge(vertexStart, vertex);

            var neighbors = graph.GetAllNeighborsOf(vertexStart);

            //Assert
            Assert.IsTrue(isAllAdded);
            Assert.AreEqual(vertices.Count, neighbors.Count());

            //check opposite edge - non directed graph
            foreach(var vertex in vertices)
            {
                neighbors = graph.GetAllNeighborsOf(vertex);
                Assert.IsTrue(neighbors.Contains(vertexStart));
            }

        }

        [TestMethod]
        public void AddEdgeFromOneToAll_DirectedGraph_Test()
        {
            //Arrange
            var vertexStart = new Vertex<char>('0');
            var vertices = CreateTestVertices();
            var graph = new Graph<char>(isDirected: true);

            graph.AddVertex(vertexStart);

            foreach (var v in vertices)
                graph.AddVertex(v);

            //Act
            bool isAllAdded = true;
            foreach (var vertex in vertices)
                isAllAdded &= graph.AddEdge(vertexStart, vertex);

            var neighbors = graph.GetAllNeighborsOf(vertexStart);

            //Assert
            Assert.IsTrue(isAllAdded);
            Assert.AreEqual(vertices.Count, neighbors.Count());

            //check opposite edge - non directed graph
            foreach (var vertex in vertices)
            {
                neighbors = graph.GetAllNeighborsOf(vertex);
                Assert.IsFalse(neighbors.Contains(vertexStart));
            }
        }

        #endregion

        #region Helper Methods
        public static List<Vertex<char>> CreateTestVertices()
        {
            var abc = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            List<Vertex<char>> vertices = new List<Vertex<char>>();

            foreach(var c in abc)
            {
                vertices.Add(new Vertex<char>(c));
            }

            return vertices;
        }

        #endregion
    }
}
