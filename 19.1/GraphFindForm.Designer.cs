namespace _19._1
{
    partial class GraphFindForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileLoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsTryCastMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolsMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(624, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileSaveMenuItem,
            this.FileLoadMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileMenuItem.Text = "Файл";
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileSaveMenuItem.Text = "Сохранить граф";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileLoadMenuItem
            // 
            this.FileLoadMenuItem.Name = "FileLoadMenuItem";
            this.FileLoadMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileLoadMenuItem.Text = "Загрузить граф";
            this.FileLoadMenuItem.Click += new System.EventHandler(this.FileLoadMenuItem_Click);
            // 
            // ToolsMenuItem
            // 
            this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsOpenMenuItem,
            this.ToolsTryCastMenuItem});
            this.ToolsMenuItem.Name = "ToolsMenuItem";
            this.ToolsMenuItem.Size = new System.Drawing.Size(141, 20);
            this.ToolsMenuItem.Text = "Панель инструментов";
            // 
            // ToolsOpenMenuItem
            // 
            this.ToolsOpenMenuItem.Name = "ToolsOpenMenuItem";
            this.ToolsOpenMenuItem.Size = new System.Drawing.Size(207, 22);
            this.ToolsOpenMenuItem.Text = "Открыть";
            this.ToolsOpenMenuItem.Click += new System.EventHandler(this.ToolsOpenMenuItem_Click);
            // 
            // ToolsTryCastMenuItem
            // 
            this.ToolsTryCastMenuItem.Name = "ToolsTryCastMenuItem";
            this.ToolsTryCastMenuItem.Size = new System.Drawing.Size(207, 22);
            this.ToolsTryCastMenuItem.Text = "Преобразовать к дереву";
            this.ToolsTryCastMenuItem.Click += new System.EventHandler(this.ToolsTryCastMenuItem_Click);
            // 
            // GraphFindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "GraphFindForm";
            this.Text = "Сведение к дереву";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphFindForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphFindForm_MouseClick);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileLoadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsTryCastMenuItem;
    }
}

