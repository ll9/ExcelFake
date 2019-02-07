using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Models
{
    class SDComboboxColumn: SDColumn
    {
        public SDComboboxColumn()
        {
        }

        public SDComboboxColumn(string name, string dataType, bool synchronize, string sDDataTableId) : base(name, dataType, synchronize, sDDataTableId)
        {
        }

        public SDComboboxColumn(string id, string name, string dataType, bool synchronize, string sDDataTableId) : base(id, name, dataType, synchronize, sDDataTableId)
        {
        }

        public string ComboboxValues { get; set; }
    }
}
