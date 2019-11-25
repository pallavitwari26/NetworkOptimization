using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Enum
    {
        public enum VertexStatus {
                UNSEEN=Int32.MaxValue,
                FRINGE=0,
                INTREE=1
        };

        public enum VertexColor
        {
             
            WHITE = 0,
            GREY = 1,
            BLACK = 2

        }

    }
}
