using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19._2
{
    public partial class EulerGraphForm : Form
    {

        private ToolsForm ToolsWindow; //окошко с инструментами

        private Library.EulerGraph.Graph Graph; //граф

        private Graphics FormGraphics;

        public static SelectedTool Tool; //какой инструмент выбран

        private int SelectedVertexX = -1;
        private int SelectedVertexY = -1;


        private void EulerGraphForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Tool == SelectedTool.None)
                {
                    Graph.DeSelectVertex(SelectedVertexX, SelectedVertexY);
                    SelectedVertexX = -1;
                    SelectedVertexY = -1;
                    //FormGraphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                    FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                    return;
                }
                //добавление вершины
                if (Tool == SelectedTool.AddVertex)
                {
                    Graph.DeSelectVertex(SelectedVertexX, SelectedVertexY);
                    SelectedVertexX = -1;
                    SelectedVertexY = -1;
                    //есть уже объект на данном месте ?
                    if (!Graph.AddVertex(e.X, e.Y))
                    {
                        MessageBox.Show("Нельзя накладывать вершины друг на друга.");
                        return;
                    }

                    //будет ли проверка для ребра ?
                    //FormGraphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                    FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                }
                //добавление ребра
                if (Tool == SelectedTool.AddEdge)
                {
                    //проверим, щелкнули мы на вершину
                    if (Graph.IsVertex(e.X, e.Y))
                    {
                        if ((SelectedVertexX == -1) && (SelectedVertexY == -1))
                        {
                            Graph.SelectVertex(e.X, e.Y);
                            SelectedVertexX = e.X;
                            SelectedVertexY = e.Y;
                            //FormGraphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                            FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                            return;
                        }
                        else
                        {
                            uint weight = 0;
                            try
                            {
                                weight = uint.Parse(InputBox.Show("Введите вес ребра :"));
                                if (weight == 0)
                                    throw new Exception("Вес ребра должен быть больше 0.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                Graph.DeSelectVertex(SelectedVertexX, SelectedVertexY);
                                FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                                SelectedVertexX = -1;
                                SelectedVertexY = -1;
                                return;
                            }
                            Graph.AddEdge(SelectedVertexX, SelectedVertexY, e.X, e.Y, weight);
                            Graph.DeSelectVertex(SelectedVertexX, SelectedVertexY);
                            //FormGraphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                            FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                            SelectedVertexX = -1;
                            SelectedVertexY = -1;
                        }
                    }
                }
                if (Tool == SelectedTool.DeleteObject)
                {

                    Graph.DeSelectVertex(SelectedVertexX, SelectedVertexY);
                    SelectedVertexX = -1;
                    SelectedVertexY = -1;
                    //определим, что удаляем
                    if (Graph.IsVertex(e.X, e.Y))
                        Graph.DeleteVertex(e.X, e.Y);
                    else
                        Graph.DeleteEdge(e.X, e.Y);
                    //FormGraphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                    FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                }
                if (Tool == SelectedTool.AllDelete)
                {
                    SelectedVertexX = -1;
                    SelectedVertexY = -1;
                    Graph.ClearAll();
                    FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
                }

            }
        }

        public EulerGraphForm()
        {
            InitializeComponent();
            ToolsWindow = new ToolsForm();
            Graph = new Library.EulerGraph.Graph(this.Width, this.Height);
            FormGraphics = this.CreateGraphics();
            ToolsWindow.Show();
        }

        private void SaveFileMainMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            if (sd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Выберете файл для сохранения.");
                return;
            }
            try
            {
                if (!Graph.Save(sd.FileName))
                {
                    MessageBox.Show("Невозможно сохранить граф. Он пуст.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadFileMainMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Выберете файл для открытия.");
                return;
            }
            try
            {
                if (!Graph.Load(od.FileName))
                {
                    MessageBox.Show("Невозможно загрузить файл. Файл не граф или поврежден.");
                    return;
                }
                FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenPanelToolsMainMenuItem_Click(object sender, EventArgs e)
        {
            if (ToolsWindow.IsDisposed)
            {
                ToolsWindow = new ToolsForm();
                ToolsWindow.Show();
            }
        }

        private void FindCycleToolsMainMenuItem_Click(object sender, EventArgs e)
        {
            if (Graph.IsEmpty())
            {
                MessageBox.Show("Нет связей либо граф не имеет вершин.");
                return;
            }
            string path = "";
            try
            {
                MessageBox.Show("Минимальная сумма всех ребер : " + Graph.FindCycle(ref path).ToString());
                MessageBox.Show("Пройденный путь : " + path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EulerGraphForm_Paint(object sender, PaintEventArgs e)
        {
            FormGraphics.DrawImage(Graph.GraphMap, 0, 0);
        }
    }
}
