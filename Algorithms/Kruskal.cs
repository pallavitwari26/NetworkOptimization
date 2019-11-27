using Graphs;
using Heap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Kruskal
    {
        #region Properties
        public int[] Color { get; set; }
        public int[] Dad { get; set; }
        public int[] Bandwidth { get; set; }
        public int[] Rank { get; set; }
        public KruskalMaxHeap Heap { get; set; }
        public int[] BFSDad { get; set; }
        public Graph MSTGraph { get; set; }
        public int BW { get; private set; }

        Stopwatch watch;
        double elapsed;

        #endregion
        #region Constructor

        public Kruskal(int number)
        {
            Color = new int[number];
            Dad = new int[number];
            Rank = new int[number];
            Bandwidth = new int[number];

        }

        #endregion
        #region Methods
        public Graph GenerateMST(Graph graph, int source, int destination)
        {

            elapsed = 0;
            watch = Stopwatch.StartNew();
            try
            {
                int vertices = graph.NumberOfVertices;
                int edges = graph.NumberOfEdges;
                Heap = new KruskalMaxHeap(edges*2);
                MSTGraph = new Graph(vertices);

                for (int v = 0; v < graph.NumberOfVertices; v++)
                {
                    List<Edge> lstEdges = graph.Adjacent[v];
                    foreach (Edge e in lstEdges)
                    {                    
                        Heap.Insert(e);
                    }
                }

                
                //Intialize rank and dad for each vertex
                for (int i = 0; i < vertices; i++)
                {
                    Dad[i] = i;
                    Rank[i] = 1;
                }

                for (int k = 0; k < edges; k++)
                {

                    Edge e = Heap.ExtractMaximum();
                    int rank1 = Find(e.Start);
                    int rank2 = Find(e.End);
                    if (rank1 != rank2)
                    {
                        MSTGraph.AddEdge(e.Start, e.End, e.Weight);
                        Union(rank1, rank2);
                    }

                }
                //MSTGraph.PrintGraph();


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;

            }
            watch.Stop();
            elapsed = watch.Elapsed.TotalSeconds;
            Console.WriteLine("Kruskal : Time to calculate create MST = " + elapsed);

            return MSTGraph;
        }

        public int Find(int vertex)
        {
            int w = vertex;
            while (Dad[w] != w)
            {
                w = Dad[w];
            }
            return w;
        }

        public void Union(int root1, int root2)
        {
            if (Rank[root1] > Rank[root2])
            {
                Dad[root2] = root1;
            }
            else if (Rank[root1] < Rank[root2])
            {
                Dad[root1] = root2;
            }
            else
            {
                Dad[root1] = root2;
                Rank[root2]++;
            }
        }

        public int BFS(Graph graph, int source, int destination)
        {

            elapsed = 0;
            watch = Stopwatch.StartNew();
            //Console.Write("Path : ");
            try
            {
                int vertices = graph.NumberOfVertices;
                Color = new int[vertices];
                BFSDad = new int[vertices];
                Bandwidth = new int[vertices];

                for (int i = 0; i < vertices; i++)
                {
                    Color[i] = (int)Graphs.Enum.VertexColor.WHITE;
                    Bandwidth[i] = int.MaxValue;
                    BFSDad[i] = -1;
                }
                Color[source] = (int)Graphs.Enum.VertexColor.GREY;
                Dad[source] = 0;
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(source);

                while (Color[destination] != (int)Graphs.Enum.VertexColor.BLACK && queue.Count != 0)
                {
                    int v = queue.Dequeue();
                    List<Edge> neighbors = new List<Edge>(graph.Adjacent[v]);
                    foreach (Edge e in neighbors)
                    {
                        int w = e.GetAdjacentVertex(v);
                        if (Color[w] == (int)Graphs.Enum.VertexColor.WHITE)
                        {
                            Color[w] = (int)Graphs.Enum.VertexColor.GREY;
                            Bandwidth[w] = Math.Min(Bandwidth[w], e.Weight);
                            BFSDad[w] = v;
                            queue.Enqueue(w);
                        }
                        else if (Color[w] == (int)Graphs.Enum.VertexColor.GREY && Bandwidth[w] < Math.Min(Bandwidth[v], e.Weight))
                        {
                            BFSDad[w] = v;
                            Bandwidth[w] = Math.Min(Bandwidth[v], e.Weight);

                        }
                    }
                    Color[v] = (int)Graphs.Enum.VertexColor.BLACK;

                    //Console.Write(v + " ");
                }

                
            }

            catch (Exception ex)
            {
                Console.Write("Error in BFS. Message =" + ex.Message);
            }
            watch.Stop();
            elapsed = watch.Elapsed.TotalSeconds;
            Console.WriteLine("Kruskal : Time for BSF = " +elapsed);
            return Bandwidth[destination];
        }

        #endregion
    }
}
