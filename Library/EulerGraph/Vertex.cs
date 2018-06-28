using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EulerGraph
{
    internal class Vertex
    {
        public int X;
        public int Y;
        public int GlobalIndex;
        public bool IsVisited;
        public List<Edge> Links;

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
            Links = new List<Edge>();
        }
    }
}
