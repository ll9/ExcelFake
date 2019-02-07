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
        private ApplicationDbContext context;
        private AdoContext adoContext;
        private DbTableRepository dbTableRepository;

        public SyncTableFacade()
        {
            context = new ApplicationDbContext();
            adoContext = new AdoContext();
            dbTableRepository = new DbTableRepository(adoContext);
        }

        public void AddTable(SDDataTable table)
        {
            //var table = new SDDataTable("table1", true, new List<SDColumn>
            //{
            //    new SDTextBoxColumn("col1", typeof(int).ToString(), true),
            //    new SDTextBoxColumn("col2", typeof(string).ToString(), true)
            //});



            dbTableRepository.Add(table);
            context.SDDataTables.Add(table);

            // TODO: Sync

            // IF Sync Successfull
            //context.SDStatuses.Add(new SDStatus(table.Id));
            //foreach (var column in table.Columns)
            //{
            //    context.SDStatuses.Add(new SDStatus(column.Id));
            //}

        }

        public void RemoveTable(SDDataTable table)
        {
            dbTableRepository.Remove(table);
            context.SDDataTables.Remove(table);

            // TODO: Sync

            // IF Sync Successfull
            //context.SDStatuses.Remove(new SDStatus(table.Id));
            //foreach (var column in table.Columns)
            //{
            //    context.SDStatuses.Remove(new SDStatus(column.Id));
            //}

        }
    }
}
