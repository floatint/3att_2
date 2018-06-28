using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Library.EulerGraph
{
    public class Graph
    {
        private List<Vertex> Vertexes;

        //битмап графа
        public Bitmap GraphMap;
        private Drawing.Render Render;

        private int VertexFreeSpaceMul = 6;
        private int EdgeWidth = 3;
        private int VertexRadius = 20;

        //----------------------------LOGIC-------------------------------

        //если нет ребер или вершин
        public bool IsEmpty()
        {
            if ((Vertexes.Count == 0) || (GetEdgesCount() == 0))
                return true;
            return false;
        }




        private uint FindBackPath(Vertex v, Vertex fv, List<Vertex> path)
        {
            path.Add(v);
            if (v == fv)
                return 0;
            uint result = 0;


            ////если у текущей вершины в соседях непосредственно есть конечная веришна
            foreach (var n in v.Links)
            {
                if (n.Vertex == fv)
                {
                    path.Add(n.Vertex);
                    return n.Weight;
                }
            }

            //инае ищем ближайший путь

            Edge e = null;
            List<Edge> edges = new List<Edge>();
            //найдем все не посященные вершины
            foreach (var n in v.Links)
            {
                if (!n.IsVisited)
                    edges.Add(n);
            }
            //если таковых нет, то отберем минимальную из посещенных
            if (edges.Count == 0)
            {
                //отберем все те, которые посетили менее 2х раз
                foreach (var i in v.Links)
                {
                    if (i.VisitCount < 2)
                        edges.Add(i);
                }
                e = GetMinimum(edges);
            }
            else
                //получим ребро с минимальным весом
                e = GetMinimum(edges);
            result += e.Weight;
            e.IsVisited = true;
            e.VisitCount++;
            Edge e2 = GetBackEdge(e);
            e2.IsVisited = true;
            e2.VisitCount++;
            result += FindBackPath(e.Vertex,fv, path);

            return result;
        }

        //поиск цикла, обходящего все ребра
        private uint Find(Vertex v, Vertex fv, List<Vertex> path, int edgesCnt)
        {
            uint result = 0;
            //path.Add(v); //добавляем в путь
            if (edgesCnt < 1)
            {
                CleanUp();
                return result + FindBackPath(v, fv, path);
            }
            path.Add(v); //добавляем в путь
            List<Edge> edgesList = new List<Edge>();
            //получим список непосещенных ребер
            foreach (var i in v.Links)
            {
                if (!i.IsVisited)
                    edgesList.Add(i);
            }
            Edge e = null;
            bool IsVisitedPath = false; //опредяляем, нужно ли уменьшать кол-во посещенных ребер
            //если уже посетили все веришны
            if (edgesList.Count == 0)
            {
                //найдем минимум из всех посещенных
                foreach (var i in v.Links)
                {
                    if (i.VisitCount < 2)
                        edgesList.Add(i);
                }
                e = GetMinimum(edgesList);
                IsVisitedPath = true;
            }
            else //если есть не посещенные
            {
                //то опять же, выберем минимум
                e = GetMinimum(edgesList);
                IsVisitedPath = false;
            }
            ////пометим найденное ребро как пройденное (и обратное ему)
            e.IsVisited = true;
            e.VisitCount++;
            Edge e2 = GetBackEdge(e);
            e2.IsVisited = true;
            e2.VisitCount++;
            //увеличиваем вес результата
            result += e.Weight;
            if (IsVisitedPath)
                result += Find(e.Vertex, fv, path, edgesCnt);
            else
                result += Find(e.Vertex, fv, path, edgesCnt - 1);
            return result;
        }

        public uint FindCycle(ref string cycle)
        {
            List<Vertex> vl = new List<Vertex>();
            uint result = Find(Vertexes[0], Vertexes[0], vl, GetEdgesCount());
            foreach (var v in vl)
            {
                cycle += v.GlobalIndex.ToString() + ' ';
            }
            return result;
        }
        //---------------------------------------------------------------



        //-------------------------data manipulation---------------------



        private Edge GetBackEdge(Edge e)
        {
            //находим вершину, от которой идет e ребро
            Vertex foundVertex = null;
            foreach (var v in Vertexes)
            {
                foreach (var i in v.Links)
                {
                    if (i == e)
                        foundVertex = v;
                }
            }
            //ищем найденную вершину как конечную точку ребра из вершины e.Vertex
            foreach (var l in e.Vertex.Links)
            {
                if (l.Vertex == foundVertex)
                    return l;
            }
            return null;
        }
        private int GetEdgesCount()
        {
            CleanUp();
            int result = 0;
            //проходим все вершины
            foreach (var v in Vertexes)
            {
                //смотрим список ребер
                foreach (var e in v.Links)
                {
                    Edge t = null;
                    //найдем обратное ребро для данного
                    foreach (var a in e.Vertex.Links)
                    {
                        if (a.Vertex == v)
                        {
                            t = a;
                            break;
                        }
                    }
                    if ((t.IsVisited) || (e.IsVisited))
                        continue;
                    else
                    {
                        result++;
                        e.IsVisited = true;
                    }
                }
            }
            CleanUp();
            return result;
        }

        private void CleanUp()
        {
            foreach (var v in Vertexes)
            {
                foreach (var n in v.Links)
                {
                    n.IsVisited = false;
                    n.VisitCount = 0;
                }
            }
        }

        //удаление графа
        public void ClearAll()
        {
            Vertexes.Clear();
            Render.Clear();
        }

        //получить вершину по координатам
        private Vertex GetVertex(int x, int y)
        {
            foreach (var n in Vertexes)
            {
                if (Math.Pow((n.X - x), 2) + Math.Pow((n.Y - y), 2) <= VertexRadius * VertexRadius)
                {
                    return n;
                }
            }
            //exception may be
            return null;
        }

        //получаем ребро по координатам
        private Edge GetEdge(int x, int y)
        {
            foreach (var v in Vertexes)
            {
                foreach (var e in v.Links)
                {
                    //(x-x1)/(x2-x1) = (y-y1)/(y2-y1)
                    int dx1 = e.Vertex.X - v.X;
                    int dy1 = e.Vertex.Y - v.Y;

                    int dx = x - v.X;
                    int dy = y - v.Y;

                    int S = dx1 * dy - dx * dy1;
                    //((x-x1)/(y-y1))=((x-x2)/(y-y2))
                    //if ((x - v.X) / (y - v.Y) == (x - e.Vertex.X) / (y - e.Vertex.Y))
                    //    return e;
                   
                }
            }
            return null;
        }

        //нажали на ребро ?
        public bool IsEdge(int x, int y)
        {
            if (GetEdge(x, y) == null)
                return false;
            return true;
        }


        //WARNING!!!
        //изменение веса у ребра
        public void ChangeWeight(int x, int y, uint weight)
        {
            Edge e = GetEdge(x, y);
            e.Weight = weight;
            Vertex v = null;
            //найдем, откуда идет эта вершина
            foreach (var n in Vertexes)
            {
                foreach (var i in n.Links)
                {
                    if (i == e)
                    {
                        v = n;
                        break;
                    }
                }
                if (v != null)
                    break;
            }
            //найдем вершину, обратную e
            Edge e2 = null;
            foreach (var n in v.Links)
            {
                if (n.Vertex == e.Vertex)
                {
                    e2 = n;
                    break;
                }
            }
            e2.Weight = weight;
            Render.ReDraw(Vertexes);
        }

        //нажали на вершину ?
        public bool IsVertex(int x, int y)
        {
            foreach (var n in Vertexes)
            {
                if (Math.Pow((n.X - x), 2) + Math.Pow((n.Y - y), 2) <= VertexRadius * VertexRadius)
                {
                    return true;
                }
            }
            return false;
        }


        //получаем ребро с минимальным весом
        private Edge GetMinimum(List<Edge> links)
        {
            uint min = 9999999;
            Edge result = null;
            foreach (var e in links)
            {
                if (e.Weight < min)
                {
                    min = e.Weight;
                    result = e;
                }
            }
            return result;
        }


        //проверка наличия свободного места для новой вершины
        private bool IsExistVertex(int x, int y)
        {
            foreach (var n in Vertexes)
            {
                if (Math.Pow((n.X - x), 2) + Math.Pow((n.Y - y), 2) <= (VertexRadius * VertexRadius) * VertexFreeSpaceMul)
                {
                    return true;
                }
            }
            return false;
        }

        //отрисовка "выбора" вершины
        public void SelectVertex(int x, int y)
        {
            Render.DrawSelectedVertex(GetVertex(x, y));
        }

        public void DeSelectVertex(int x, int y)
        {
            Render.DrawVertex(GetVertex(x, y));
        }

        //добавить вершину
        public bool AddVertex(int x, int y)
        {
            if (IsExistVertex(x, y))
                return false;
            Vertex tmp = new Vertex(x, y);
            Vertexes.Add(tmp);
            tmp.GlobalIndex = Vertexes.Count - 1;
            Render.DrawVertex(tmp);
            return true;
        }

        public void DeleteVertex(int x, int y)
        {
            Vertex dV = null;
            //находим удаляемую вершину
            foreach (var v in Vertexes)
            {
                //нашли ту, на которую кликнули
                if (Math.Pow((v.X - x), 2) + Math.Pow((v.Y - y), 2) <= VertexRadius * VertexRadius)
                {
                    dV = v;
                }
            }
            if (dV == null)
                return;
            //бежим по всем вершинам
            foreach (var v in Vertexes)
            {
                //заходим в соседи
                for (int i = v.Links.Count - 1; i >= 0; i--)
                {
                    if (v.Links[i].Vertex == dV)
                    {
                        //удаляем из соседей нужную вершину
                        v.Links.RemoveAt(i);
                    }
                }
                //правим GlobalIndex
                if (v.GlobalIndex > dV.GlobalIndex)
                    v.GlobalIndex--;
            }
            //удаляем из основного списка
            Vertexes.Remove(dV);
            //redraw
            Render.ReDraw(Vertexes);
            return;
        }


        //добавить ребро
        public void AddEdge(int v1x, int v1y, int v2x, int v2y, uint weight)
        {
            Vertex v1 = GetVertex(v1x, v1y);
            Vertex v2 = GetVertex(v2x, v2y);
            if ((v1 == null) || (v2 == null))
                return;
            v1.Links.Add(new Edge(v2, weight));
            v2.Links.Add(new Edge(v1, weight));
            Render.DrawEdge(v1, v1.Links[v1.Links.Count - 1]);
            //перерисуем конечную вершину, т.к. первая перерисовывается из главной формы
            Render.DrawVertex(v2);
        }

        public void DeleteEdge(int x, int y)
        {
            Vertex tmp1 = null; //в глобальном массиве
            Vertex tmp2 = null; //сосед
            //бежим по списку вершин
            foreach (var v in Vertexes)
            {
                tmp1 = v;
                //заходим к соседям
                foreach (var n in v.Links)
                {
                    //получаем точки двух вершин
                    int v1x = v.X;
                    int v1y = v.Y;
                    int v2x = n.Vertex.X;
                    int v2y = n.Vertex.Y;
                    //проверка щелчка на линию
                    if (((x - v1x) * (v2y - v1y) / (v2x - v1x) + v1y) <= (y + EdgeWidth) &&
                        ((x - v1x) * (v2y - v1y) / (v2x - v1x) + v1y) >= (y - EdgeWidth))
                    {
                        if ((v1x <= v2x && v1x <= x && x <= v2x) ||
                            (v1x >= v2x && v1x >= x && x >= v2x))
                        {
                            //получаем соседа, с которым связаны
                            tmp2 = n.Vertex;
                            break;
                        }
                    }
                }
                if (tmp2 != null)
                    break;
            }
            if ((tmp1 != null) && (tmp2 != null))
            {
                for (int i = tmp1.Links.Count - 1; i >= 0; i--)
                {
                    if (tmp1.Links[i].Vertex == tmp2)
                    {
                        tmp1.Links.RemoveAt(i);
                        break;
                    }
                }
                for (int i = tmp2.Links.Count - 1; i >= 0; i--)
                {
                    if (tmp2.Links[i].Vertex == tmp1)
                    {
                        tmp2.Links.RemoveAt(i);
                        break;
                    }
                }
                //tmp1.Neighbours.Remove(tmp2);
                //tmp2.Neighbours.Remove(tmp1);
            }
            Render.ReDraw(Vertexes);
        }

        ////добавить ребро
        //public void AddEdge(int v1x, int v1y, int v2x, int v2y, uint weight)
        //{
        //    Vertex v1 = GetVertex(v1x, v1y);
        //    Vertex v2 = GetVertex(v2x, v2y);
        //    if ((v1 == null) || (v2 == null))
        //        return;
        //    v1.Links.Add(new Edge(v2, weight));
        //    v2.Links.Add(new Edge(v1, weight));
        //    //Edges.Add(new Edge(v1, v2, weight));
        //    //v1.Neighbours.Add(v2);
        //    //v2.Neighbours.Add(v1);
        //    //Render.DrawEdge(v1, v2);
        //    //перерисуем конечную вершину, т.к. первая перерисовывается из главной формы
        //    //Render.DrawVertex(v2);
        //}

        public bool Save(string path)
        {
            if (Vertexes.Count == 0)
                return false;
            FileStream WriteStream = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(WriteStream, Encoding.Unicode);
            //запись сигнатуры
            bw.Write("CSF_Euler");
            //запись кол-ва вершин
            bw.Write(Vertexes.Count);
            //запись вершин
            foreach (var v in Vertexes)
            {
                bw.Write(v.GlobalIndex);
                bw.Write(v.X);
                bw.Write(v.Y);
            }
            foreach (var v in Vertexes)
            {
                bw.Write(v.Links.Count);
                foreach (var e in v.Links)
                {
                    bw.Write(e.Vertex.GlobalIndex);
                    bw.Write(e.Weight);
                }

            }
            bw.Close();
            return true;
        }


        public bool Load(string path)
        {
            FileStream WriteStream = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(WriteStream, Encoding.Unicode);
            if (br.ReadString() != "CSF_Euler")
                return false;
            Vertexes.Clear();
            //Edges.Clear();
            Render.Clear();
            int VertCount = br.ReadInt32();
            if (VertCount == 0)
                return false;
            //начинаем читать вершины
            for (int i = 0; i < VertCount; i++)
            {
                //прочитали вершину
                int GlobalIndex = br.ReadInt32();
                Vertexes.Add(new Vertex(br.ReadInt32(), br.ReadInt32()));
                Vertexes[i].GlobalIndex = GlobalIndex;
            }
            //читаем соседей
            foreach (var v in Vertexes)
            {
                //init list
                v.Links = new List<Edge>();
                int EdgesCount = br.ReadInt32();
                if (EdgesCount != 0)
                {
                    for (int i = 0; i < EdgesCount; i++)
                    {
                        v.Links.Add(new Edge(Vertexes[br.ReadInt32()], br.ReadUInt32()));
                    }
                }
            }
            br.Close();
            Render.ReDraw(Vertexes);
            return true;
        }



        public Graph(int width, int height)
        {
            Vertexes = new List<Vertex>();
            GraphMap = new Bitmap(width, height);
            Render = new Drawing.Render(GraphMap);
        }
    }
}
