using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Models
{
    class SDSchemaObject
    {
        public SDSchemaObject(  )
        {
        }

        public SDSchemaObject(ICollection<SDDataTable> sDDataTables, ICollection<SDColumn> columns)
        {
            SDDataTables = sDDataTables;
            SDColumns = columns;
        }

        public ICollection<SDDataTable> SDDataTables { get; set; } = new List<SDDataTable>();
        public ICollection<SDColumn> SDColumns { get; set; } = new List<SDColumn>();
    }
}
