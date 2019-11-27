using Graphs;
using Heap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Enum = Graphs.Enum;

namespace Algorithms
{
    public class Dijkstra
    {

        #region Properties
        Stopwatch watch;
        double elapsed;

        public int[] Status { get; set; }
        public int[] Dad { get; set; }
        public int[] Bandwidth { get; set; }

        #endregion

      
        #region Methods
        public int MaximumBandWidth(Graph graph,int source,int destination)
        {
            elapsed = 0;
            watch = Stopwatch.StartNew();
            Dad = new int[graph.NumberOfVertices];
            Bandwidth = new int[graph.NumberOfVertices];
            Status = new int[graph.NumberOfVertices];        
            int vertices = graph.NumberOfVertices;
            try
            {

                for (int i = 0; i < vertices; i++)
                {
                    Status[i] = (int)Enum.VertexStatus.UNSEEN;
                }

                Status[source] = (int)Enum.VertexStatus.INTREE;             
               // Console.Write("Path : "+ source + " ");
                Dad[source] = 0;
                Bandwidth[source] = 0;

                List<Edge> adjacentEdges = graph.Adjacent[source];
                foreach (Edge edge in adjacentEdges)
                {
                    int v = edge.GetAdjacentVertex(source);
                    Status[v] = (int)Enum.VertexStatus.FRINGE;
                    Dad[v] = source;
                    Bandwidth[v] = edge.Weight;
                }
                int count = 0;
                while (Status[destination] != (int)Enum.VertexStatus.INTREE)
                {
                    count++;
                    int maxBandwidth = Int32.MinValue, maximumIndex = -1;
                    // Finding the best fringe i.e. the one with maximum weight
                    for (int i = 0; i < vertices; i++)
                    {
                       
                        if (Status[i] == (int)Enum.VertexStatus.FRINGE)
                        {
                            if (Bandwidth[i] > maxBandwidth)
                            {
                                maxBandwidth = Bandwidth[i];
                                maximumIndex = i;
                            }
                        }
                    }
                    Status[maximumIndex] = (int)Enum.VertexStatus.INTREE;
                    //Console.Write(maximumIndex + " ");


                    List<Edge> verticesTowardsMax = graph.Adjacent[maximumIndex];
                    foreach (Edge edge in verticesTowardsMax)
                    {
                        int v = edge.GetAdjacentVertex(maximumIndex);
                       
                        if (Status[v] == (int)Enum.VertexStatus.UNSEEN)
                        {
                            Dad[v] = maximumIndex;
                            Status[v] = (int)Enum.VertexStatus.FRINGE;
                            Bandwidth[v] = Math.Min(Bandwidth[maximumIndex], edge.Weight);
                        }
                        else if (Status[v] == (int)Enum.VertexStatus.FRINGE && Bandwidth[v] < Math.Min(Bandwidth[maximumIndex], edge.Weight))
                        {
                            Dad[v] = maximumIndex;
                            Bandwidth[v] = Math.Min(Bandwidth[maximumIndex], edge.Weight);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
         
            watch.Stop();
            elapsed = watch.Elapsed.TotalSeconds;
            Console.WriteLine("Dijkstra : Time to calculate maximum bandwidth = " + elapsed);
            return Bandwidth[destination];





        }

        public int MaximumBWUsingHeap(Graph graph, int source, int destination)
        {
            elapsed = 0;
            var watch = Stopwatch.StartNew();
            Dad = new int[graph.NumberOfVertices];
            Bandwidth = new int[graph.NumberOfVertices];
            Status = new int[graph.NumberOfVertices];
            int vertices = graph.NumberOfVertices;
            MaxHeap maxHeap = new MaxHeap(graph.NumberOfVertices);         
            try
            {

                for (int i = 0; i < vertices; i++)
                {
                    Status[i] = (int)Enum.VertexStatus.UNSEEN;
                }

                Status[source] = (int)Enum.VertexStatus.INTREE;
               // Console.Write("Path : " +source + " ");
                Dad[source] = 0;
                Bandwidth[source] = 0;

                List<Edge> adjacentEdges = graph.Adjacent[source];
                foreach (Edge edge in adjacentEdges)
                {
                    int v = edge.GetAdjacentVertex(source);
                    Status[v] = (int)Enum.VertexStatus.FRINGE;
                    Dad[v] = source;
                    Bandwidth[v] = edge.Weight;
                    maxHeap.Insert(v,Bandwidth[v]);
                }

                int count = 0;
               
                while(!maxHeap.IsEmpty()) // while there are fringes
                {
                    count++;
                    int maximumIndex = -1;
                    maximumIndex = maxHeap.ExtractMaximum();          
                    Status[maximumIndex] = (int)Enum.VertexStatus.INTREE;

                   // Console.Write(maximumIndex + " ");


                    List<Edge> verticesTowardsMax = graph.Adjacent[maximumIndex];
                    foreach (Edge edge in verticesTowardsMax)
                    {
                        int v = edge.GetAdjacentVertex(maximumIndex);
                        if (Status[v] == (int)Enum.VertexStatus.UNSEEN)
                        {
                            Dad[v] = maximumIndex;
                            Status[v] = (int)Enum.VertexStatus.FRINGE;
                            Bandwidth[v] = Math.Min(Bandwidth[maximumIndex], edge.Weight);
                            maxHeap.Insert(v, Bandwidth[v]);
                        }
                        else if (Status[v] == (int)Enum.VertexStatus.FRINGE && Bandwidth[v] < Math.Min(Bandwidth[maximumIndex], edge.Weight))
                        {
                            Dad[v] = maximumIndex;
                            Bandwidth[v] = Math.Min(Bandwidth[maximumIndex], edge.Weight);
                            maxHeap.Modify(v,Bandwidth[v]);
                         
                        }
                    }
                
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
                
            }

            watch.Stop();
            elapsed = watch.Elapsed.TotalSeconds;
            Console.WriteLine("Dijkstra : Time to calculate maximum bandwidth using Heap = " + elapsed);
            return Bandwidth[destination];
        }
        #endregion
    }
}
