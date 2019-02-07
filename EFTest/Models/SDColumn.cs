using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Models
{
    class SDColumn
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public Type DataType { get; set; }
        public bool Synchronize { get; set; }

        [ForeignKey(nameof(SDDataTable))]
        public string SDDataTableId { get; set; }
    }
}
