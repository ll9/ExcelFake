namespace EFTest.Views
{
    partial class AddTableDialog
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
            System.Windows.Forms.Label nameLabel;
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.columnViewModelsDataGridView = new System.Windows.Forms.DataGridView();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.addTableViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnViewModelsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.columnViewModelsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addTableViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnViewModelsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(38, 29);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(38, 13);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addTableViewModelBindingSource, "Name", true));
            this.nameTextBox.Location = new System.Drawing.Point(82, 26);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(210, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // columnViewModelsDataGridView
            // 
            this.columnViewModelsDataGridView.AutoGenerateColumns = false;
            this.columnViewModelsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnViewModelsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.dataTypeDataGridViewTextBoxColumn});
            this.columnViewModelsDataGridView.DataBindings.Add(new System.Windows.Forms.Binding("DataSource", this.addTableViewModelBindingSource, "ColumnViewModels", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.columnViewModelsDataGridView.DataSource = this.columnViewModelsBindingSource;
            this.columnViewModelsDataGridView.Location = new System.Drawing.Point(41, 61);
            this.columnViewModelsDataGridView.Name = "columnViewModelsDataGridView";
            this.columnViewModelsDataGridView.Size = new System.Drawing.Size(251, 220);
            this.columnViewModelsDataGridView.TabIndex = 2;
            this.columnViewModelsDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.columnViewModelsDataGridView_DefaultValuesNeeded);
            // 
            // OKButton
            // 
            this.OKButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.addTableViewModelBindingSource, "IsValid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.OKButton.Location = new System.Drawing.Point(136, 297);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(217, 297);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Abbrechen";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // addTableViewModelBindingSource
            // 
            this.addTableViewModelBindingSource.DataSource = typeof(EFTest.ViewModels.AddTableViewModel);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // dataTypeDataGridViewTextBoxColumn
            // 
            this.dataTypeDataGridViewTextBoxColumn.DataPropertyName = "DataType";
            this.dataTypeDataGridViewTextBoxColumn.HeaderText = "DataType";
            this.dataTypeDataGridViewTextBoxColumn.Name = "dataTypeDataGridViewTextBoxColumn";
            this.dataTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // columnViewModelsBindingSource
            // 
            this.columnViewModelsBindingSource.DataSource = typeof(EFTest.ViewModels.ColumnViewModel);
            // 
            // AddTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 331);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.columnViewModelsDataGridView);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Name = "AddTableDialog";
            this.Text = "AddTableDialog";
            ((System.ComponentModel.ISupportInitialize)(this.columnViewModelsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addTableViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnViewModelsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource addTableViewModelBindingSource;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.BindingSource columnViewModelsBindingSource;
        private System.Windows.Forms.DataGridView columnViewModelsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button OKButton;
        private new System.Windows.Forms.Button CancelButton;
    }
}