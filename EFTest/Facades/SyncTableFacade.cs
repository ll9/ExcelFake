using EFTest.Data;
using EFTest.Models;
using EFTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Facades
{
    class SyncTableFacade
    {
        public void AddTable(SDDataTable table)
        {
            //var table = new SDDataTable("table1", true, new List<SDColumn>
            //{
            //    new SDTextBoxColumn("col1", typeof(int).ToString(), true),
            //    new SDTextBoxColumn("col2", typeof(string).ToString(), true)
            //});

            var context = new ApplicationDbContext();
            var adoContext = new AdoContext();
            var dbTableRepository = new DbTableRepository(adoContext);

            dbTableRepository.Add(table);
            context.SDDataTables.Add(table);


        }
    }
}
