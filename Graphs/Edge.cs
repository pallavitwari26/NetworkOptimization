using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Edge
    {

        #region variables

        public int Start { get; set; }
        public int End { get; set; }
        public int Weight { get; set; }

        #endregion

        #region Constructor
        public Edge(int start, int end, int weight)
        {
            this.Start = start;
            this.End = end;
            this.Weight = weight;
        }

        #endregion
        public int GetVertex(int x)
        {
            return Start;
        }

        public int GetAdjacentVertex(int x)
        {
            if (x == Start)
                return End;
            else
                return Start;
        }


      public  bool EdgeExists( Edge e,List<Edge> lstEdges)
        {
            foreach (var edge in lstEdges)
            {
                if (edge.Start == e.Start && edge.End == e.End)
                    return true;
                if (edge.End == e.Start && edge.Start == e.End)
                    return true;

            }

            return false;
        }

        override
        public string ToString()
        {
            return "[" + Start + "," + End + "," + Weight + "]";
        }



    }
}
