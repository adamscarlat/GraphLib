using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLibs;
using GraphLibs.GraphExtensions;

namespace GraphLibUnitTests
{
    [TestClass]
    public class GraphAlgorithmsExtensionParallelBFS_Tests
    {
        #region Test Methods

        [TestMethod]
        public void ParallelBFSTest_Positive()
        {
            //Arrange
            var graph = GenerateLargeGraph_Tree(100, 10);

            //Act
            var result = graph.ParallelBreadthFirstSearch(0, 73);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParallelBFSTest_BottomUp_Positive()
        {
            //Arrange
            var graph = GenerateLargeGraph_Tree(1000, 10);

            //Act
            var result = graph.ParallelBreadthFirstSearch(99, 2);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParallelBFSTest_VeryLargeGraph_Positive()
        {
            //Arrange
            var graph = GenerateLargeGraph_Tree(1000000, 1000);

            //Act
            var result = graph.ParallelBreadthFirstSearch(0, 100000);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParallelBFSTest_VeryLargeGraph_BFS_Positive()
        {
            //Arrange
            var graph = GenerateLargeGraph_Tree(1000000, 1000);

            //Act
            var result = graph.BreadthFirstSearch(0, 100000);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region Helper Methods

        private Graph<int> GenerateLargeGraph_CompleteGraph(int numNodes)
        {
            var graph = new Graph<int>();

            for (int i = 0; i < numNodes; i++)
                for (int j = 0; j < numNodes; j++)
                    graph.AddEdge(i, j);

            return graph;
        }

        private static Graph<int> GenerateLargeGraph_Tree(int numNodes, int childrenPerNode)
        {
            numNodes /= childrenPerNode;
            var graph = new Graph<int>();

            for (int i = 0; i < numNodes; i++)
            {
                int layer = i + 1;
                for (int j = (layer - 1) * childrenPerNode; j < layer * childrenPerNode; j++)
                {
                    graph.AddEdge(i, j);
                }
            }
            return graph;
        }

        #endregion
    }
}
