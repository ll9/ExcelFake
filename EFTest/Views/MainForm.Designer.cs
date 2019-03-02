namespace EFTest
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GridTabControl = new System.Windows.Forms.TabControl();
            this.ColumnMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.spalteEinfügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spalteLöschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTableButton = new System.Windows.Forms.Button();
            this.ColumnMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridTabControl
            // 
            this.GridTabControl.Location = new System.Drawing.Point(12, 12);
            this.GridTabControl.Name = "GridTabControl";
            this.GridTabControl.SelectedIndex = 0;
            this.GridTabControl.Size = new System.Drawing.Size(743, 297);
            this.GridTabControl.TabIndex = 0;
            // 
            // ColumnMenuStrip
            // 
            this.ColumnMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spalteEinfügenToolStripMenuItem,
            this.spalteLöschenToolStripMenuItem});
            this.ColumnMenuStrip.Name = "ColumnMenuStrip";
            this.ColumnMenuStrip.Size = new System.Drawing.Size(157, 48);
            // 
            // spalteEinfügenToolStripMenuItem
            // 
            this.spalteEinfügenToolStripMenuItem.Name = "spalteEinfügenToolStripMenuItem";
            this.spalteEinfügenToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.spalteEinfügenToolStripMenuItem.Text = "Spalte einfügen";
            this.spalteEinfügenToolStripMenuItem.Click += new System.EventHandler(this.spalteEinfügenToolStripMenuItem_Click);
            // 
            // spalteLöschenToolStripMenuItem
            // 
            this.spalteLöschenToolStripMenuItem.Name = "spalteLöschenToolStripMenuItem";
            this.spalteLöschenToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.spalteLöschenToolStripMenuItem.Text = "Spalte löschen";
            this.spalteLöschenToolStripMenuItem.Click += new System.EventHandler(this.spalteLöschenToolStripMenuItem_Click);
            // 
            // AddTableButton
            // 
            this.AddTableButton.Location = new System.Drawing.Point(795, 56);
            this.AddTableButton.Name = "AddTableButton";
            this.AddTableButton.Size = new System.Drawing.Size(94, 23);
            this.AddTableButton.TabIndex = 2;
            this.AddTableButton.Text = "Add Table";
            this.AddTableButton.UseVisualStyleBackColor = true;
            this.AddTableButton.Click += new System.EventHandler(this.AddTableButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 439);
            this.Controls.Add(this.AddTableButton);
            this.Controls.Add(this.GridTabControl);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ColumnMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl GridTabControl;
        private System.Windows.Forms.ContextMenuStrip ColumnMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem spalteEinfügenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spalteLöschenToolStripMenuItem;
        private System.Windows.Forms.Button AddTableButton;
    }
}

