using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class GraphGenerator
    {
       Random random;
       Stopwatch watch;
       double elapsedS;
       int totalEdges;
       int sumOfDegrees = 0;

        public int RandomNext { get; set; }

        public GraphGenerator()
        {
           random = new Random();
           totalEdges = 0;
           RandomNext = 100;
        }

        public void GenerateConnectedGraph(Graph g)
        {
            totalEdges = 0;
            int n = g.NumberOfVertices;
            int randomWeight = 0;
    
            for (int i = 0; i < n; i++)
            {
                randomWeight = random.Next(RandomNext)+1;  // avoid getting a 0
                if (i + 1 > n - 1)
                {
                    break;
                   
                }               

                    g.Adjacent[i].Add(new Edge(i, i+1, randomWeight));
                    g.Adjacent[i+1].Add(new Edge(i+1,i, randomWeight));
                    g.Degree[i]++;
                    g.Degree[i+1]++;
                    totalEdges++;                  
            }

            randomWeight = random.Next(RandomNext)+1;
            g.Adjacent[n - 1].Add(new Edge(n - 1, 0, randomWeight));
            g.Adjacent[0].Add(new Edge(0,n-1, randomWeight));
            g.Degree[n-1]++;
            g.Degree[0]++;        
            totalEdges++;

        }

        public Graph GenerateSparseGraph(Graph graph, int vertices, int degree)
        {
            elapsedS = 0;
            watch = Stopwatch.StartNew();
          
            try
            { 
            GenerateConnectedGraph(graph);
            
           while(totalEdges<(vertices*degree)/2)
            {
                int source = random.Next(vertices);
                int destination = random.Next(vertices);
                int weight = random.Next(RandomNext)+1;
                if (graph.Degree[source] < degree && graph.Degree[destination] < degree && source != destination)
                {
                    Edge edge = new Edge(source, destination, weight);
                    if (!edge.EdgeExists(edge, graph.Adjacent[source]))
                    {
                        graph.AddEdge(source, destination, weight);
                        graph.Degree[source]++;
                        graph.Degree[destination]++;
                        totalEdges++;
                    }
                }
            }

          
            watch.Stop();
            Console.WriteLine("Generate Undirected Sparse Graph");
            graph.NumberOfEdges = totalEdges;
            Console.WriteLine("Total Edges in Undirected Sparse Graph = " + graph.NumberOfEdges);
                graph.NumberOfEdges = totalEdges; //use for kruskal
                for (int i = 0; i < vertices; i++)
                {
                    sumOfDegrees += graph.Adjacent[i].Count;
                }
            Console.WriteLine("Average degree = " + Convert.ToDouble(sumOfDegrees/vertices));

            elapsedS = watch.Elapsed.TotalSeconds;
            Console.WriteLine("Time to generate undirected sparse graph =  " + Math.Round(elapsedS,6));
            //graph.PrintGraph();
            }
             catch (Exception e)
            {
                Console.WriteLine("Error in graph generation. Message= " + e.Message);

            }
            return graph;
        }

        public  Graph GenerateDenseGraph(Graph graph,int vertices,int percentage)
        {
            elapsedS = 0;
            graph.NumberOfEdges = 0;
            watch = Stopwatch.StartNew();           
            sumOfDegrees = 0;
            try
            {
                GenerateConnectedGraph(graph);
                for (int i = 0; i < vertices; i++)
                {
                    for (int j = i + 1; j < vertices; j++)
                    {
                        int probability = random.Next(100)+1;
                        int randomWeight = random.Next(RandomNext) + 1;
                        if (probability <= percentage)
                        {
                            Edge e = new Edge(i, j, randomWeight);
                            if (!e.EdgeExists(e, graph.Adjacent[i]))
                            {
                                graph.AddEdge(i, j, randomWeight);
                                graph.Degree[i]++;
                                graph.Degree[j]++;
                                totalEdges++;
                            }
                        }
                    }
                }
                watch.Stop();
                elapsedS = watch.Elapsed.TotalSeconds;
                Console.WriteLine();
                Console.WriteLine("Generate Undirected Dense Graph");
                graph.NumberOfEdges = totalEdges; // use for kruskal
                Console.WriteLine("Total Edges in Undirected Dense Graph = " + graph.NumberOfEdges);
              
                for (int i = 0; i < vertices; i++)
                {
                    sumOfDegrees += graph.Adjacent[i].Count;
                }
          
                Console.WriteLine("Average degree = " + sumOfDegrees / vertices);
                Console.WriteLine("Time to generate undirected dense graph =  " + Math.Round(elapsedS,6));
                //graph.PrintGraph();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in graph generation. Message= " + e.Message);
            }
            return graph;

        }


        
    }
}
