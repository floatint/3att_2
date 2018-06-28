using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EulerGraph
{
    internal class Edge
    {
        public Vertex Vertex;
        //public Vertex v2;
        public uint Weight;
        public bool IsVisited;
        public uint VisitCount;

        public Edge(Vertex v, uint weight)
        {
            this.Vertex = v;
            Weight = weight;
        }
    }
}
