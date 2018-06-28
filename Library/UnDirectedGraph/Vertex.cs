using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal enum VertexState
    {
        NotVisited, Visited, Visiting
    }

    internal class Vertex
    {
        public int X;
        public int Y;
        public int GlobalIndex;
        public bool IsVisited;
        
        public List<Vertex> Neighbours;

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
            Neighbours = new List<Vertex>();
        }
    }
}
