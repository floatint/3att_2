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
    public partial class InputBox : Form
    {
        private static string result;
        private static InputBox ib;

        public InputBox()
        {
            InitializeComponent();
        }

        public static string Show(string inputBoxText)
        {
            ib = new InputBox();
            ib.RequestLabel.Text = inputBoxText;
            ib.ShowDialog();
            return result;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            result = InputTextBox.Text;
            ib.Dispose();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            result = string.Empty;
            ib.Dispose();
        }
    }
}
