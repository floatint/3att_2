using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Library.Drawing
{
    internal class VertexRender
    {

        public static void Draw(Vertex v, Graphics graphics, Pen borderPen, SolidBrush fillBrush, Font textFont, SolidBrush textBrush, int R)
        {
            graphics.DrawEllipse(borderPen, (v.X - R), (v.Y - R), 2 * R, 2 * R);
            graphics.FillEllipse(fillBrush, (v.X - R), (v.Y - R), 2 * R, 2 * R);
            graphics.DrawString(v.GlobalIndex.ToString(), textFont, textBrush, v.X - 9, v.Y - 9);
        }

        //for euler
        public static void Draw(EulerGraph.Vertex v, Graphics graphics, Pen borderPen, SolidBrush fillBrush, Font textFont, SolidBrush textBrush, int R)
        {
            graphics.DrawEllipse(borderPen, (v.X - R), (v.Y - R), 2 * R, 2 * R);
            graphics.FillEllipse(fillBrush, (v.X - R), (v.Y - R), 2 * R, 2 * R);
            graphics.DrawString(v.GlobalIndex.ToString(), textFont, textBrush, v.X - 9, v.Y - 9);
        }

        //public static void Draw(Vertex vertex, Graphics graphics, Color borderColor, Color fillColor, string text)
        //{
        //    graphics.DrawEllipse(new Pen(borderColor, 3), (vertex.X - vertex.R), (vertex.Y - vertex.R), 2 * vertex.R, 2 * vertex.R);
        //    graphics.FillEllipse(new SolidBrush(fillColor), (vertex.X - vertex.R), (vertex.Y - vertex.R), 2 * vertex.R, 2 * vertex.R);
        //    graphics.DrawString(text, new Font("Arial", 12), new SolidBrush(Color.Black), vertex.X - 9, vertex.Y - 9);
        //}
    }
}
