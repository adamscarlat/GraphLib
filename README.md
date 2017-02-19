# GraphLib
Graph library written in C#. The library makes it possible to model a graph data structure by adding vertices and edges. The current implementation of the graph is with an adjacency list.
There are extension methods to the graph that enable various graph algorithms such as Breadth First Search and Depth First Search. The graph data structure is generic and it can contain any type of .NET object. In addition, the library is fully tested (see unittests project).

## Using the Library
To use the library import GraphLibs into your project, instantiate a new graph:

![Alt Text](https://github.com/adamscarlat/GraphLib/blob/master/images/newGraph.gif)

and add vertices and edges:

![Alt Text](https://github.com/adamscarlat/GraphLib/blob/master/images/addVertices.gif)

Using the graph library it is also possible to check the neighbors of a vertex in the graph. To get the neighbors of a vertex in the graph:

`` IEnumerable<Person> neighbors = graph.GetAllNeighborsOf(p1); ``

## Graph Algorithms
The library offers numerous graph algorithms that are added as extension methods. 

To use the graph algorthims first import the extension method class: ``using GraphLibs.GraphExtensions``. The graph algorithms will be added to the graph API. 

### Breadth First Search

### Depth First Search
