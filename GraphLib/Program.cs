using GraphLibs;
using GraphLibs.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * TODO
 * 0.) (Done) Refactor GraphAlgorithmsExtension - wrapper class for queue and stack, dup code
 * 1.) Add sample config method (to add edges as tuples) in graph with tests
 * 2.) Add Routing algorithms (in a seperate class) - add as extension methods
 *      - (Done) BFS 
 *      - Parallel BFS
 *      - (Done) DFS
 *      - Parallel DFS
 *      - Bipartitnes
 *      - Dijkstra
 *      - Minimum Spanning Tree
 *      - Cycle detection
 * 3.) Add probabilistic algorithms (in a seperate class) - add as extension methods
 *      - Markov chains
 *      - Hidden markov models (?)
 * 4.) Persist graph
 * 5.) JSON API
 * 6.) LINQ like API (e.g. GetAllNeighborsThat(predicate..))
 * 7.) ...
 */


namespace GraphLib
{
    class Program
    {
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

        static char[] abc = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public static List<Vertex<char>> CreateTestVertices()
        {
            List<Vertex<char>> vertices = new List<Vertex<char>>();

            foreach (var c in abc)
            {
                vertices.Add(new Vertex<char>(c));
            }

            return vertices;
        }

        public static Graph<char> CreateSampleGraph(List<Tuple<char, char>> graphConfig)
        {
            var graph = new Graph<char>();
            var vertices = CreateTestVertices();
            graph.AddVertices(vertices);

            foreach (var tuple in graphConfig)
            {
                graph.AddEdge(tuple.Item1, tuple.Item2);
            }

            return graph;

        }

        private static Graph<int> GenerateLargeCompleteGraph(int numNodes)
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

        static void Main(string[] args)
        {
            //var graph = CreateSampleGraph(sampleEdgeConfig_tree);

            //graph.ParallelBreadthFirstSearch<char>('A', 'N');

            //graph.PrintGraph();

            var graph = GenerateLargeGraph_Tree(1000, 2);
            graph.PrintGraph();

            Console.ReadLine();

        }
    }
}
