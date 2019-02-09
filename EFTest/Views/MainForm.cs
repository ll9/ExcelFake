using EFTest.Controllers;
using EFTest.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFTest
{
    public partial class MainForm : Form
    {
        private MainController _controller;

        public MainForm()
        {
            InitializeComponent();

            _controller = new MainController(this);
            _controller.LoadData();
        }

        public void AddGrid(DataTable dataTable)
        {
            var tab = new TabPage(dataTable.TableName);
            var dataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = dataTable
            };
            dataGrid.ColumnHeaderMouseClick += DataGrid_ColumnHeaderMouseClick;

            tab.Controls.Add(dataGrid);
            GridTabControl.TabPages.Add(tab);
        }

        private void DataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is DataGridView dataGrid)
            {

                var hitTest = dataGrid.HitTest(e.X, e.Y);

                if (e.Button == MouseButtons.Right)
                {
                    if (hitTest.RowIndex == -1)
                    {
                        ColumnMenuStrip.Show(dataGrid, dataGrid.PointToClient(Cursor.Position));
                    }
                }
            }
        }

        private void spalteEinfügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddColumnDialog().ShowDialog();
        }
    }
}
