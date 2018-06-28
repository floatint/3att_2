namespace _19._1
{
    partial class ToolsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DeselectButton = new System.Windows.Forms.Button();
            this.DeleteAllButton = new System.Windows.Forms.Button();
            this.DeleteObjectButton = new System.Windows.Forms.Button();
            this.AddNewEdgeButton = new System.Windows.Forms.Button();
            this.AddNewVertexButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DeselectButton
            // 
            this.DeselectButton.Image = global::_19._1.Properties.Resources.cursor;
            this.DeselectButton.Location = new System.Drawing.Point(3, 2);
            this.DeselectButton.Name = "DeselectButton";
            this.DeselectButton.Size = new System.Drawing.Size(46, 47);
            this.DeselectButton.TabIndex = 4;
            this.DeselectButton.Tag = "0";
            this.DeselectButton.UseVisualStyleBackColor = true;
            this.DeselectButton.Click += new System.EventHandler(this.DeselectButton_Click);
            // 
            // DeleteAllButton
            // 
            this.DeleteAllButton.Image = global::_19._1.Properties.Resources.deleteAll;
            this.DeleteAllButton.Location = new System.Drawing.Point(211, 2);
            this.DeleteAllButton.Name = "DeleteAllButton";
            this.DeleteAllButton.Size = new System.Drawing.Size(46, 47);
            this.DeleteAllButton.TabIndex = 3;
            this.DeleteAllButton.Tag = "4";
            this.DeleteAllButton.UseVisualStyleBackColor = true;
            this.DeleteAllButton.Click += new System.EventHandler(this.DeleteAllButton_Click);
            // 
            // DeleteObjectButton
            // 
            this.DeleteObjectButton.Image = global::_19._1.Properties.Resources.delete;
            this.DeleteObjectButton.Location = new System.Drawing.Point(159, 2);
            this.DeleteObjectButton.Name = "DeleteObjectButton";
            this.DeleteObjectButton.Size = new System.Drawing.Size(46, 47);
            this.DeleteObjectButton.TabIndex = 2;
            this.DeleteObjectButton.Tag = "3";
            this.DeleteObjectButton.UseVisualStyleBackColor = true;
            this.DeleteObjectButton.Click += new System.EventHandler(this.DeleteObjectButton_Click);
            // 
            // AddNewEdgeButton
            // 
            this.AddNewEdgeButton.Image = global::_19._1.Properties.Resources.edge;
            this.AddNewEdgeButton.Location = new System.Drawing.Point(107, 2);
            this.AddNewEdgeButton.Name = "AddNewEdgeButton";
            this.AddNewEdgeButton.Size = new System.Drawing.Size(46, 47);
            this.AddNewEdgeButton.TabIndex = 1;
            this.AddNewEdgeButton.Tag = "2";
            this.AddNewEdgeButton.UseVisualStyleBackColor = true;
            this.AddNewEdgeButton.Click += new System.EventHandler(this.AddNewEdgeButton_Click);
            // 
            // AddNewVertexButton
            // 
            this.AddNewVertexButton.Image = global::_19._1.Properties.Resources.vertex;
            this.AddNewVertexButton.Location = new System.Drawing.Point(55, 2);
            this.AddNewVertexButton.Name = "AddNewVertexButton";
            this.AddNewVertexButton.Size = new System.Drawing.Size(46, 47);
            this.AddNewVertexButton.TabIndex = 0;
            this.AddNewVertexButton.Tag = "1";
            this.AddNewVertexButton.UseVisualStyleBackColor = true;
            this.AddNewVertexButton.Click += new System.EventHandler(this.AddNewVertexButton_Click);
            // 
            // ToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 51);
            this.Controls.Add(this.DeselectButton);
            this.Controls.Add(this.DeleteAllButton);
            this.Controls.Add(this.DeleteObjectButton);
            this.Controls.Add(this.AddNewEdgeButton);
            this.Controls.Add(this.AddNewVertexButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ToolsForm";
            this.Text = "Инструменты";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddNewVertexButton;
        private System.Windows.Forms.Button AddNewEdgeButton;
        private System.Windows.Forms.Button DeleteObjectButton;
        private System.Windows.Forms.Button DeleteAllButton;
        private System.Windows.Forms.Button DeselectButton;
    }
}