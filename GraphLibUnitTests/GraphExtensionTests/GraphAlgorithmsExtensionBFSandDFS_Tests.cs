using GraphLibs;
using GraphLibs.GraphExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GraphLibUnitTests
{
    [TestClass]
    public class GraphAlgorithmsExtensionBFSandDFS_Tests
    {
        #region Test Methods

        #region BFS

        [TestMethod]
        public void BreadthFirstSearch_LoopedGraph_Positive()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('A', 'E');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_LoopedGraph_Negative()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('A', 'Z');

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_LoopedGraph_Positive_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('E', 'A');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_LoopedGraph_Negative_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('Z', 'A');

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_Tree_Positive()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('A', 'E');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_Tree_Negative()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('A', 'Z');

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_Tree_Positive_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('E', 'A');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BreadthFirstSearch_Tree_Negative_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.BreadthFirstSearch('Z', 'A');

            //Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region DFS

        [TestMethod]
        public void DepthhFirstSearch_LoopedGraph_Positive()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('A', 'E');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DepthFirstSearch_LoopedGraph_Negative()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('A', 'Z');

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DepthFirstSearch_LoopedGraph_Positive_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('E', 'A');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DepthFirstSearch_LoopedGraph_Negative_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('Z', 'A');

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DepthFirstSearch_Tree_Positive()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('A', 'E');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DepthFirstSearch_Tree_Negative()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('A', 'Z');

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DepthFirstSearch_Tree_Positive_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('E', 'A');

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DepthFirstSearch_Tree_Negative_BottomUp()
        {
            //Arrange
            var graph = CreateSampleGraph(sampleEdgeConfig_loop);

            //Act
            var result = graph.DepthFirstSearch('Z', 'A');

            //Assert
            Assert.IsFalse(result);
        }

        #endregion

        #endregion


        #region Helper Methods

        public static Graph<char> CreateSampleGraph(List<Tuple<char, char>> graphConfig)
        {
            var graph = new Graph<char>();

            foreach (var tuple in graphConfig)
            {
                graph.AddEdge(tuple.Item1, tuple.Item2);
            }

            return graph;

        }

        #endregion


        #region Sample Graph Configurations

        static List<Tuple<char, char>> sampleEdgeConfig_loop =
        new List<Tuple<char, char>>
        {
                new Tuple<char, char>('A', 'B'),
                new Tuple<char, char>('A', 'C'),
                new Tuple<char, char>('A', 'D'),
                new Tuple<char, char>('D', 'E'),
                new Tuple<char, char>('D', 'F'),
                new Tuple<char, char>('D', 'G'),
                new Tuple<char, char>('D', 'H'),
                new Tuple<char, char>('E', 'B'),
                new Tuple<char, char>('H', 'G'),
        };


        static List<Tuple<char, char>> sampleEdgeConfig_tree =
        new List<Tuple<char, char>>
        {
                new Tuple<char, char>('A', 'B'),
                new Tuple<char, char>('A', 'C'),
                new Tuple<char, char>('A', 'D'),
                new Tuple<char, char>('B', 'E'),
                new Tuple<char, char>('B', 'F'),
                new Tuple<char, char>('C', 'G'),
                new Tuple<char, char>('D', 'H'),
                new Tuple<char, char>('D', 'J'),
                new Tuple<char, char>('D', 'K'),
                new Tuple<char, char>('G', 'L'),
                new Tuple<char, char>('G', 'M'),
                new Tuple<char, char>('G', 'N'),
        };

        #endregion

    }
}
