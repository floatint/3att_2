using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;



//При удалении вершины не стираются ребра
//При каких то действиях перерисовывается только одна вершина. вторая остается с хвостом

namespace Library
{
    public enum CastingState
    {
        AlreadyTree, CanCast, CantCast, NoGraph
    }

    public class Graph
    {
        private List<Vertex> Vertexes;
        private Vertex DeletedVertex;

        //битмап графа
        public Bitmap GraphMap;
        private Drawing.Render Render;
        //для интерактивности
        private int VertexFreeSpaceMul = 6;
        private int EdgeWidth = 3;
        private int VertexRadius = 20;

        //делает вершины "новыми"
        private void CleanUp()
        {
            foreach (var n in Vertexes)
            {
                n.IsVisited = false;
            }
            DeletedVertex = null;
            //for (int i = 0; i < Vertexes.Count; i++)
            //{
            //    Vertexes[i].IsVisited = false;
            //}
            //DeletedVertex = null;
        }


        //ищем цикл
        private List<Vertex> FindCycle(Stack<Vertex> stack, Vertex v, Vertex p)
        {
            List<Vertex> result = new List<Vertex>();

            if (v == DeletedVertex)
                return result;
            //if (v == p)
            //    return result;
            if (v.IsVisited)
            {
                //раскрутка стека в список вершин, задействованных в цикле
                Vertex vt = stack.Pop();
                while (vt != v)
                {
                    result.Add(vt);
                    vt = stack.Pop();
                }
                result.Add(vt);
                return result;
            }
            else
            {
                v.IsVisited = true;
                stack.Push(v);
                foreach (var n in v.Neighbours)
                {
                    if (n == p)
                        continue;
                    result = FindCycle(stack, n, v);
                    if (result.Count != 0)
                        return result;
                }
                stack.Pop();
            }
            return result;
        }

        //обход всех вершин 
        private void Walk(Vertex v, Vertex p)
        {
            if (v == null)
                return;
            if (v == p)
                return;
            if (v.IsVisited)
                return;
            if (v == DeletedVertex)
                return;
            v.IsVisited = true;
            foreach (var n in v.Neighbours)
                Walk(n, v);

        }

        //граф связан ?
        private bool IsLinked()
        {
            //полагаем, что граф уже обнулен
            Vertex v = null;
            //получим любую вершину, которая не помечена как удаленная
            foreach (var n in Vertexes)
            {
                if (n == DeletedVertex)
                    continue;
                v = n;
                break;
            }
            Walk(v, null); //обходим
            //проверяем IsVisited
            foreach (var n in Vertexes)
            {
                if (n == DeletedVertex)
                    continue;
                if (!n.IsVisited)
                    return false;
            }
            CleanUp(); //уберем за собой
            return true;
        }

        //кол-во висящих вершин
        private int GetPendVertexCount(ref Vertex oneVertex)
        {
            int result = 0;
            foreach (var v in Vertexes)
            {
                if (v.Neighbours.Count == 0)
                {
                    oneVertex = v;
                    result++;
                }
            }
            return result;
        }

        public CastingState TryCast()
        {
            if (Vertexes.Count == 0)
                return CastingState.NoGraph;
            CleanUp(); //чистка
            //стек для обработанных верщин
            Stack<Vertex> stack = new Stack<Vertex>();
            List<Vertex> cycle = FindCycle(stack, Vertexes[0], null);
            CleanUp(); //чистка
            //вдруг уже дерево ?
            if ((cycle.Count == 0) && (IsLinked()))
                return CastingState.AlreadyTree;
            else //если не дерево
            {
                Vertex pendedVert = null;
                if (GetPendVertexCount(ref pendedVert) > 1)
                {
                    return CastingState.CantCast;
                }
                else
                {
                    if (cycle.Count == 0)
                    {
                        DeletedVertex = pendedVert;
                        Render.DrawDeletedLinks(DeletedVertex);
                        //CleanUp();
                        return CastingState.CanCast;
                    }
                }
                //то перебираем все вершины цикла
                for (int n = 0; n < cycle.Count; n++)
                {
                    DeletedVertex = cycle[n]; //помечаем текущую как удаленную
                    stack.Clear(); //очищаем стек
                    List<Vertex> cycle2 = new List<Vertex>(); //новый список для поиска новых циклов
                    //запустим поиск цикла
                    if (n > 0)
                        cycle2 = FindCycle(stack, cycle[n - 1], null);
                    if (n == 0)
                        cycle2 = FindCycle(stack, cycle[1], null);
                    //если можно одним удалением свести
                    Vertex tmp = DeletedVertex; // запомним удаленную вершину, т.к. IsLinked() вызывает CleanUp()
                    CleanUp(); // чистим
                    DeletedVertex = tmp; //восстанавливаем
                    if ((cycle2.Count == 0) && (IsLinked()))
                    {
                        Render.DrawDeletedLinks(tmp);
                        //CleanUp();
                        return CastingState.CanCast;
                    }
                    //подчистим на всякий
                    CleanUp();
                }
            }
            return CastingState.CantCast;
        }

        //удаление графа
        public void ClearAll()
        {
            Vertexes.Clear();
            DeletedVertex = null;
            Render.Clear();
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
            //for (int i = 0; i < Vertexes.Count; i++)
            //{
            //    if (Math.Pow((Vertexes[i].X - x), 2) + Math.Pow((Vertexes[i].Y - y), 2) <= VertexRadius * VertexRadius)
            //    {
            //        return true;
            //    }
            //}
            //return false;
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
            //for (int i = 0; i < Vertexes.Count; i++)
            //{
            //    if (Math.Pow((Vertexes[i].X - x), 2) + Math.Pow((Vertexes[i].Y - y), 2) <= (VertexRadius * VertexRadius) * VertexFreeSpaceMul)
            //    {
            //        return true;
            //    }
            //}
            //return false;
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


        //удалить вершину
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
                for(int i = v.Neighbours.Count - 1; i >= 0; i--)
                {
                    if (v.Neighbours[i] == dV)
                    {
                        //удаляем из соседей нужную вершину
                        v.Neighbours.Remove(dV);
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
        public void AddEdge(int v1x, int v1y, int v2x, int v2y)
        {
            Vertex v1 = GetVertex(v1x, v1y);
            Vertex v2 = GetVertex(v2x, v2y);
            if ((v1 == null) || (v2 == null))
                return;
            v1.Neighbours.Add(v2);
            v2.Neighbours.Add(v1);
            Render.DrawEdge(v1, v2);
            //перерисуем конечную вершину, т.к. первая перерисовывается из главной формы
            Render.DrawVertex(v2);
        }

        //удалить ребро
        public void DeleteEdge(int x, int y)
        {
            Vertex tmp1 = null; //в глобальном массиве
            Vertex tmp2 = null; //сосед
            //бежим по списку вершин
            foreach (var v in Vertexes)
            {
                tmp1 = v;
                //заходим к соседям
                foreach (var n in v.Neighbours)
                {
                    //получаем точки двух вершин
                    int v1x = v.X;
                    int v1y = v.Y;
                    int v2x = n.X;
                    int v2y = n.Y;
                    //проверка щелчка на линию
                    if (((x - v1x) * (v2y - v1y) / (v2x - v1x) + v1y) <= (y + EdgeWidth) &&
                        ((x - v1x) * (v2y - v1y) / (v2x - v1x) + v1y) >= (y - EdgeWidth))
                    {
                        if ((v1x <= v2x && v1x <= x && x <= v2x) ||
                            (v1x >= v2x && v1x >= x && x >= v2x))
                        {
                            //получаем соседа, с которым связаны
                            tmp2 = n;
                            break;
                        }
                    }
                }
                if (tmp2 != null)
                    break;
            }
            if ((tmp1 != null) && (tmp2 != null))
            {
                tmp1.Neighbours.Remove(tmp2);
                tmp2.Neighbours.Remove(tmp1);
            }
            Render.ReDraw(Vertexes); 
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
            //for (int i = 0; i < Vertexes.Count; i++)
            //{
            //    if (Math.Pow((Vertexes[i].X - x), 2) + Math.Pow((Vertexes[i].Y - y), 2) <= VertexRadius * VertexRadius)
            //    {
            //        return Vertexes[i];
            //    }
            //}
            ////exception may be
            //return null;
        }



        //сохранение графа в файл
        public bool Save(string path)
        {
            if (Vertexes.Count == 0)
                return false;
            FileStream WriteStream = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(WriteStream, Encoding.Unicode);
            //запись сигнатуры
            bw.Write("CSF");
            //запись кол-ва вершин
            bw.Write(Vertexes.Count);
            //запись вершин
            foreach (var n in Vertexes)
            {
                //записали вершину
                bw.Write(n.GlobalIndex);
                bw.Write(n.X);
                bw.Write(n.Y);
            }
            //запись индексов соседей
            foreach (var n in Vertexes)
            {
                //кол-во соседей
                bw.Write(n.Neighbours.Count);
                //запись индексов
                foreach (var v in n.Neighbours)
                {
                    bw.Write(v.GlobalIndex);
                }
            }
            bw.Close();
            return true;
        }
        
        //загрузка графа из файла
        public bool Load(string path)
        {
            FileStream WriteStream = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(WriteStream, Encoding.Unicode);
            if (br.ReadString() != "CSF")
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
                v.Neighbours = new List<Vertex>();
                int NeigboursCount = br.ReadInt32();
                if (NeigboursCount != 0)
                {
                    for (int i = 0; i < NeigboursCount; i++)
                    {
                        v.Neighbours.Add(Vertexes[br.ReadInt32()]);
                    }
                }
            }
            br.Close();
            Render.ReDraw(Vertexes);
            return true;
        }


        //construct
        public Graph(int width, int height)
        {
            Vertexes = new List<Vertex>();
            DeletedVertex = null;
            GraphMap = new Bitmap(width, height);
            Render = new Drawing.Render(GraphMap);
        }
    }
}
