using EFTest.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFTest.Views
{
    public partial class AddTableDialog : Form
    {
        internal AddTableViewModel AddTableViewModel = new AddTableViewModel();

        public AddTableDialog()
        {
            InitializeComponent();

            var items = new[]
            {
                new { Text = "Zahl", Value = typeof(double).ToString() },
                new { Text = "Datum", Value = typeof(DateTime).ToString() },
                new { Text = "Text", Value = typeof(string).ToString() },
            };
            dataTypeDataGridViewTextBoxColumn.DataSource = items;
            dataTypeDataGridViewTextBoxColumn.DisplayMember = "Text";
            dataTypeDataGridViewTextBoxColumn.ValueMember = "Value";

            addTableViewModelBindingSource.DataSource = AddTableViewModel;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void columnViewModelsDataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[nameof(dataTypeDataGridViewTextBoxColumn)].Value = typeof(string).ToString();
        }
    }
}
