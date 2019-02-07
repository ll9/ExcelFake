using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Models
{
    class SDDataTable
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Synchronize { get; set; }

        public ICollection<SDColumn> Columns { get; set; }
    }
}
