using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Library;

namespace Library.Drawing
{
    internal class EdgeRender
    {
        //just graph
        public static void Draw(Vertex v1, Vertex v2, Graphics graphics, Pen pen)
        {
            graphics.DrawLine(pen, v1.X, v1.Y, v2.X, v2.Y);
        }

        //euler graph
        public static void Draw(EulerGraph.Vertex v1, EulerGraph.Edge e, Graphics graphics, Pen pen, int R)
        {
            PointF point = new PointF((v1.X + e.Vertex.X) / 2, (v1.Y + e.Vertex.Y) / 2 + 3);
            graphics.DrawString(e.Weight.ToString(), new Font("Arial", 12), new SolidBrush(Color.Black), point);
            graphics.DrawLine(pen, v1.X, v1.Y, e.Vertex.X, e.Vertex.Y);
        }
    }
}
