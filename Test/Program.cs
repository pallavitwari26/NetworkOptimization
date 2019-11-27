using System;
using System.Diagnostics;
using Algorithms;
using Graphs;
using Heap;

namespace Test
{
    class Program
    {
        public  static int NumberOfVertices = 5000;
        public  static int iterations = 5;
        public  static int degree = 6;
        public static int percentage = 20;
        public static Random random;
        public static double elapsed=0;

        public static void Main(string[] args)
        {

            #region Maximum Bandwidth

          
  
            Console.WriteLine("All time in seconds");
            for (int i = 1; i <= iterations; i++)
            {
                random = new Random();
                GraphGenerator graphGenerator = new GraphGenerator();



                int source = random.Next(NumberOfVertices);
                int destination = random.Next(NumberOfVertices);
                while (source == destination)
                {
                    destination = random.Next(NumberOfVertices);
                }

                while (i <= 5)
                {
                    //************** Sparse graph****************///
                    Graph sparseGraph = new Graph(NumberOfVertices);
                    sparseGraph = graphGenerator.GenerateSparseGraph(sparseGraph, NumberOfVertices, degree);

                    //*************** Dense graph****************///
                    Graph denseGraph = new Graph(NumberOfVertices);
                    denseGraph = graphGenerator.GenerateDenseGraph(denseGraph, NumberOfVertices, percentage);
                    Dijkstra mb = new Dijkstra();
                    Kruskal kr = new Kruskal(NumberOfVertices);

                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("*****Maximum Bandwidth for Sparse Graph**************************");
                    Console.WriteLine("Test Case " + i + " source : " + source + ", destination : " + destination);

                    Console.WriteLine();

                    Console.WriteLine("Sparse Graph : Maximum BW from source " + source + " to destination " + destination + " =  " + mb.MaximumBandWidth(sparseGraph, source, destination));
                    Console.WriteLine("Sparse Graph : Maximum BW using MaxHeap from source " + source + " to destination  " + destination + " =  " + mb.MaximumBWUsingHeap(sparseGraph, source, destination));


                    Graph mstSparse = kr.GenerateMST(sparseGraph, source, destination);
                    Console.WriteLine("Sparse Graph : Maximum BW from source " + source + " to  destination " + destination + " =  " + kr.BFS(mstSparse, source, destination));


                    Console.WriteLine("*****Maximum Bandwidth for Sparse Graph End***********************");
                    Console.WriteLine();


                    //Console.WriteLine("*****Maximum Bandwidth for Dense Graph**************************");
                   // Console.WriteLine("Dense Graph :Maximum BW from source " + source + " to destination " + destination + " =  " + mb.MaximumBandWidth(denseGraph, source, destination));
                    //Console.WriteLine("Dense Graph : Maximum BW from source " + source + " to  destination " + destination + " =  " + mb.MaximumBWUsingHeap(denseGraph, source, destination));


                   // Graph mstDense = kr.GenerateMST(denseGraph, source, destination);
                   // Console.WriteLine("Dense Graph : Maximum BW from source " + source + " to  destination " + destination + " =  " + kr.BFS(mstDense, source, destination));


                   // Console.WriteLine("*****Maximum Bandwidth for Dense Graph End**************************");

                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine();
                    i++;
                }
            }

            #endregion

          
            Console.ReadLine();
        }
        }
    }

