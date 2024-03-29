﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Graph
    {
        #region Variables
        private int numberOfVertices;
        private int[] degree;
        private List<Edge>[] adjacent;


        public int NumberOfVertices { get => numberOfVertices; set => numberOfVertices = value; }
        public int[] Degree { get => degree; set => degree = value; }
        public List<Edge>[] Adjacent { get => adjacent; set => adjacent = value; }
        public int NumberOfEdges { get; set; }



        #endregion

        #region Constructor
        public Graph(int vertices)
        {
            if (vertices == 0)
                throw new Exception("Graph cannot be created with 0 vertices");
            this.numberOfVertices = vertices;
            this.Degree = new int[vertices];
            Adjacent = new List<Edge>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                Adjacent[i] = new List<Edge>();
                Degree[i] = 0;
            }
            this.NumberOfEdges = 0;
        
        }
        #endregion

        #region Methods
        public void AddEdge(int start, int end, int weight)
        {
            Edge edge1 = new Edge(start, end, weight);
            Adjacent[start].Add(edge1);
            Edge edge2 = new Edge(end, start, weight);
            Adjacent[end].Add(edge2);
        }

        public void PrintGraph()
        {
            for (int i = 0; i < NumberOfVertices; i++)
            {
                Console.WriteLine("i= " + i);
                List<Edge> lst = Adjacent[i];
                foreach (var item in lst)
                {
                    Console.Write(item + " ");

                }

                Console.WriteLine();
            }
        }


        public List<Edge> GetDistinctEdges(List<Edge>[] adjacentList)
        {
            List<Edge> lstDistinctEdges = new List<Edge>();

            for (int i = 0; i < adjacentList.Length; i++)
            {

                foreach (Edge edge in adjacent[i])
                {
                    Edge reverseEdge = new Edge(edge.End, edge.Start, edge.Weight);
                    if (!(lstDistinctEdges.Any(p => p.Start == reverseEdge.Start
                    && p.End == reverseEdge.End && p.Weight == reverseEdge.Weight) || lstDistinctEdges.Any(p => p.Start == edge.Start
                    && p.End == edge.End && p.Weight == edge.Weight)))
                    {
                        lstDistinctEdges.Add(edge);
                    }
                 
                }
            }
            return lstDistinctEdges;
        }
        #endregion

    }
}
