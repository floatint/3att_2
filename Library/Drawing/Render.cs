using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Library.Drawing
{
    internal class Render
    {
        Color VertexBorder;
        Color VertexFill;
        Color Edge;
        Color Text;
        Color SelectedVertexBorder;
        Color SelectedVertexFill;
        Color DeletedVertexBorder;
        Color DeletedVertexFill;
        Color DeletedEdge;

        Font TextFont;

        int VertexBorderPenSize;
        int EdgePenSize;
        int VertexRadius;
        //int TextSize;

        Graphics Canvas;
        Bitmap BitMap;


        //рисование вершины
        public void DrawVertex(Vertex v)
        {
            if (v != null)
                Drawing.VertexRender.Draw(v, Canvas, new Pen(VertexBorder, VertexBorderPenSize), new SolidBrush(VertexFill), TextFont, new SolidBrush(Text), VertexRadius);
            //v.IsDrawn = true;
        }

        public void DrawVertex(EulerGraph.Vertex v)
        {
            if (v != null)
                Drawing.VertexRender.Draw(v, Canvas, new Pen(VertexBorder, VertexBorderPenSize), new SolidBrush(VertexFill), TextFont, new SolidBrush(Text), VertexRadius);
        }

        //рисование ребра
        public void DrawEdge(Vertex v1, Vertex v2)
        {
            if ((v1 != null) && (v2 != null))
                Drawing.EdgeRender.Draw(v1, v2, Canvas, new Pen(Edge, EdgePenSize));
        }

        public void DrawEdge(EulerGraph.Vertex v1, EulerGraph.Edge e)
        {
            if ((v1 != null) && (e != null))
                Drawing.EdgeRender.Draw(v1, e, Canvas, new Pen(Edge, EdgePenSize), VertexRadius);
        }

        //рисование связки
        public void DrawLinks(Vertex v)
        {
            if (v == null)
                return;
            foreach (var n in v.Neighbours)
            {
                if (n != v)
                {
                    DrawEdge(v, n);
                    DrawVertex(n);
                }
                    //DrawEdge(v, n);
                    //Drawing.EdgeRender.Draw(v, n, Canvas, new Pen(Edge, EdgePenSize));
            }
            DrawVertex(v);
        }

        //рисование связки
        public void DrawLinks(EulerGraph.Vertex v)
        {
            if (v == null)
                return;
            foreach (var n in v.Links)
            {
                if (n.Vertex != v)
                {
                    DrawEdge(v, n);
                    DrawVertex(n.Vertex);
                }
                //DrawEdge(v, n);
                //Drawing.EdgeRender.Draw(v, n, Canvas, new Pen(Edge, EdgePenSize));
            }
            DrawVertex(v);
        }

        //рисование удаленной вершины и ее связей
        public void DrawDeletedLinks(Vertex v)
        {
            if (v == null)
                return;
            foreach (var n in v.Neighbours)
            {
                if (n != v)
                {
                    DrawDeletedEdge(v, n);
                    DrawVertex(n);
                }
                //DrawEdge(v, n);
                //Drawing.EdgeRender.Draw(v, n, Canvas, new Pen(Edge, EdgePenSize));
            }
            DrawDeletedVertex(v);
        }

        //рисование всего
        public void Draw(List<Vertex> vs)
        {
            foreach (var n in vs)
            {
                DrawLinks(n);
            }
        }

        //рисование всего
        public void Draw(List<EulerGraph.Vertex> vs)
        {
            foreach (var n in vs)
            {
                DrawLinks(n);
            }
        }

        public void ReDraw(List<Vertex> vs)
        {
            Canvas.Clear(VertexFill);
            Draw(vs);
        }

        public void ReDraw(List<EulerGraph.Vertex> vs)
        {
            Canvas.Clear(VertexFill);
            Draw(vs);
        }

        public void Clear()
        {
            Canvas.Clear(VertexFill);
        }


        public void DrawSelectedVertex(Vertex v)
        {
            if (v != null)
                Drawing.VertexRender.Draw(v, Canvas, new Pen(SelectedVertexBorder, VertexBorderPenSize), new SolidBrush(SelectedVertexFill), TextFont, new SolidBrush(Text), VertexRadius);
        }

        public void DrawSelectedVertex(EulerGraph.Vertex v)
        {
            if (v != null)
                Drawing.VertexRender.Draw(v, Canvas, new Pen(SelectedVertexBorder, VertexBorderPenSize), new SolidBrush(SelectedVertexFill), TextFont, new SolidBrush(Text), VertexRadius);
        }


        public void DrawDeletedVertex(Vertex v)
        {
            if (v != null)
                Drawing.VertexRender.Draw(v, Canvas, new Pen(DeletedVertexBorder, VertexBorderPenSize), new SolidBrush(DeletedVertexFill), TextFont, new SolidBrush(Text), VertexRadius);
        }

        public void DrawDeletedEdge(Vertex v1, Vertex v2)
        {
            if ((v1 != null) && (v2 != null))
                Drawing.EdgeRender.Draw(v1, v2, Canvas, new Pen(DeletedEdge, EdgePenSize));
        }


        
        //simple constructor.
        public Render(Bitmap bitmap)
        {
            VertexBorder = Color.Black;
            VertexFill = Color.White;
            Edge = Color.Chocolate;
            Text = Color.Black;
            SelectedVertexBorder = Color.Red;
            SelectedVertexFill = Color.Yellow;
            DeletedVertexBorder = Color.Black;
            DeletedVertexFill = Color.Red;
            DeletedEdge = Color.Red;

            TextFont = new Font("Arial", 12);

            VertexBorderPenSize = 3;
            EdgePenSize = 3;
            VertexRadius = 20;

            BitMap = bitmap;
            Canvas = Graphics.FromImage(BitMap);
        }
    }
}
