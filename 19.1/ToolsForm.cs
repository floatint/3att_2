using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19._1
{
    public enum SelectedTool
    {
        None, AddVertex, AddEdge, DeleteObject, AllDelete
    }

    public partial class ToolsForm : Form
    {
        public ToolsForm()
        {
            InitializeComponent();
        }


        private void DeselectButton_Click(object sender, EventArgs e)
        {
            //включаем все кнопки
            AddNewVertexButton.Enabled = true;
            AddNewEdgeButton.Enabled = true;
            DeleteObjectButton.Enabled = true;
            GraphFindForm.Tool = SelectedTool.None;
            //GraphFindForm.ButtonClicked = Convert.ToByte((sender as Button).Tag);
        }

        private void AddNewVertexButton_Click(object sender, EventArgs e)
        {
            DeselectButton_Click(DeselectButton, null);
            AddNewVertexButton.Enabled = false;
            GraphFindForm.Tool = SelectedTool.AddVertex;
            //GraphFindForm.ButtonClicked = Convert.ToByte((sender as Button).Tag);
        }

        private void AddNewEdgeButton_Click(object sender, EventArgs e)
        {
            DeselectButton_Click(DeselectButton, null);
            AddNewEdgeButton.Enabled = false;
            GraphFindForm.Tool = SelectedTool.AddEdge;
            //GraphFindForm.ButtonClicked = Convert.ToByte((sender as Button).Tag);
        }

        private void DeleteObjectButton_Click(object sender, EventArgs e)
        {
            DeselectButton_Click(DeselectButton, null);
            DeleteObjectButton.Enabled = false;
            GraphFindForm.Tool = SelectedTool.DeleteObject;
            //GraphFindForm.ButtonClicked = Convert.ToByte((sender as Button).Tag);
        }

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            DeselectButton_Click(DeselectButton, null);
            GraphFindForm.Tool = SelectedTool.AllDelete;
        }
    }
}
