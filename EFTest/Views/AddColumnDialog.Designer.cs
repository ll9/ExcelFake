namespace EFTest.Views
{
    partial class AddColumnDialog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label dataTypeLabel;
            System.Windows.Forms.Label nameLabel;
            this.dataTypeComboBox = new System.Windows.Forms.ComboBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.sDColumnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            dataTypeLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sDColumnBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTypeLabel
            // 
            dataTypeLabel.AutoSize = true;
            dataTypeLabel.Location = new System.Drawing.Point(12, 9);
            dataTypeLabel.Name = "dataTypeLabel";
            dataTypeLabel.Size = new System.Drawing.Size(60, 13);
            dataTypeLabel.TabIndex = 1;
            dataTypeLabel.Text = "Data Type:";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(12, 36);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(38, 13);
            nameLabel.TabIndex = 3;
            nameLabel.Text = "Name:";
            // 
            // dataTypeComboBox
            // 
            this.dataTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sDColumnBindingSource, "DataType", true));
            this.dataTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataTypeComboBox.FormattingEnabled = true;
            this.dataTypeComboBox.Location = new System.Drawing.Point(78, 6);
            this.dataTypeComboBox.Name = "dataTypeComboBox";
            this.dataTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.dataTypeComboBox.TabIndex = 2;
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sDColumnBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nameTextBox.Location = new System.Drawing.Point(78, 33);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(121, 20);
            this.nameTextBox.TabIndex = 4;
            // 
            // OKButton
            // 
            this.OKButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.sDColumnBindingSource, "IsValid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.OKButton.Location = new System.Drawing.Point(78, 76);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(40, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(124, 76);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Abbrechen";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // sDColumnBindingSource
            // 
            this.sDColumnBindingSource.DataSource = typeof(EFTest.ViewModels.ColumnViewModel);
            // 
            // AddColumnDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 111);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(dataTypeLabel);
            this.Controls.Add(this.dataTypeComboBox);
            this.Name = "AddColumnDialog";
            this.Text = "AddColumnDialog";
            ((System.ComponentModel.ISupportInitialize)(this.sDColumnBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource sDColumnBindingSource;
        private System.Windows.Forms.ComboBox dataTypeComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}