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
    public partial class Form1 : Form
    {
        private MainController _controller;

        public Form1()
        {
            InitializeComponent();

            _controller = new MainController(this);
            _controller.LoadData();
        }
    }
}
