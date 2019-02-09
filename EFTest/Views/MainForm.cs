using EFTest.Controllers;
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

            tab.Controls.Add(dataGrid);
            GridTabControl.TabPages.Add(tab);
        }
    }
}
