namespace _19._2
{
    partial class EulerGraphForm
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
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadFileMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPanelToolsMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindCycleToolsMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.инструментыToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(884, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveFileMainMenuItem,
            this.LoadFileMainMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // SaveFileMainMenuItem
            // 
            this.SaveFileMainMenuItem.Name = "SaveFileMainMenuItem";
            this.SaveFileMainMenuItem.Size = new System.Drawing.Size(132, 22);
            this.SaveFileMainMenuItem.Text = "Сохранить";
            this.SaveFileMainMenuItem.Click += new System.EventHandler(this.SaveFileMainMenuItem_Click);
            // 
            // LoadFileMainMenuItem
            // 
            this.LoadFileMainMenuItem.Name = "LoadFileMainMenuItem";
            this.LoadFileMainMenuItem.Size = new System.Drawing.Size(132, 22);
            this.LoadFileMainMenuItem.Text = "Загрузить";
            this.LoadFileMainMenuItem.Click += new System.EventHandler(this.LoadFileMainMenuItem_Click);
            // 
            // инструментыToolStripMenuItem
            // 
            this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenPanelToolsMainMenuItem,
            this.FindCycleToolsMainMenuItem});
            this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
            this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.инструментыToolStripMenuItem.Text = "Инструменты";
            // 
            // OpenPanelToolsMainMenuItem
            // 
            this.OpenPanelToolsMainMenuItem.Name = "OpenPanelToolsMainMenuItem";
            this.OpenPanelToolsMainMenuItem.Size = new System.Drawing.Size(163, 22);
            this.OpenPanelToolsMainMenuItem.Text = "Открыть панель";
            this.OpenPanelToolsMainMenuItem.Click += new System.EventHandler(this.OpenPanelToolsMainMenuItem_Click);
            // 
            // FindCycleToolsMainMenuItem
            // 
            this.FindCycleToolsMainMenuItem.Name = "FindCycleToolsMainMenuItem";
            this.FindCycleToolsMainMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FindCycleToolsMainMenuItem.Text = "Найти цикл";
            this.FindCycleToolsMainMenuItem.Click += new System.EventHandler(this.FindCycleToolsMainMenuItem_Click);
            // 
            // EulerGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 549);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "EulerGraphForm";
            this.Text = "Эйлеров цикл";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EulerGraphForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EulerGraphForm_MouseClick);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveFileMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadFileMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инструментыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenPanelToolsMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindCycleToolsMainMenuItem;
    }
}

