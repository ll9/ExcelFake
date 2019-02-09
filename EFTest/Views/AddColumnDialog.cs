using EFTest.Models;
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
    public partial class AddColumnDialog : Form
    {
        private class DisplayValue<T>
        {
            public DisplayValue(string display, T value)
            {
                Display = display;
                Value = value;
            }

            public string Display { get; set; }
            public T Value { get; set; }
        }

        public AddColumnDialog()
        {
            InitializeComponent();

            dataTypeComboBox.DataSource = new List<DisplayValue<string>>
            {
                new DisplayValue<string>("Zahl", typeof(double).ToString()),
                new DisplayValue<string>("Datum", typeof(DateTime).ToString()),
                new DisplayValue<string>("Text", typeof(string).ToString()),
            };
            dataTypeComboBox.DisplayMember = nameof(DisplayValue<object>.Display);
            dataTypeComboBox.ValueMember = nameof(DisplayValue<object>.Value);

            sDColumnBindingSource.DataSource = new SDTextBoxColumn();
        }
    }
}
