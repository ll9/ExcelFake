using EFTest.Data;
using EFTest.Models;
using EFTest.Repository;
using EFTest.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFTest.Services;

namespace EFTest.Controllers
{
    class MainController
    {
        private MainForm _view;
        private ApplicationDbContext _efContext;
        private AdoContext _adoContext;
        private DbTableRepository _dbTableRepository;
        private SyncService _syncService;

        public MainController(MainForm form1)
        {
            _view = form1;
            _efContext = new ApplicationDbContext();
            _adoContext = new AdoContext();
            _dbTableRepository = new DbTableRepository(_adoContext);
            _syncService = new SyncService(this, _efContext, _dbTableRepository);
        }

        internal void LoadData()
        {
            var sDDataTables = _efContext.SDDataTables.ToList();
            var dataTables = _dbTableRepository.List(sDDataTables);

            foreach (var dataTable in dataTables)
            {
                _view.AddGrid(dataTable);
            }
        }

        internal void AddColumn(DataTable dataTable, ColumnViewModel columnViewModel)
        {
            var sdDataTable = _efContext.SDDataTables.Single(c => c.Name == dataTable.TableName);
            var column = new SDTextBoxColumn(Guid.NewGuid().ToString(), columnViewModel.Name, columnViewModel.DataType, true, sdDataTable.Id);

            _dbTableRepository.AddColumn(dataTable.TableName, column);
            _efContext.SDColumns.Add(column);
            _efContext.SaveChanges();

            _dbTableRepository.UpdateDataTable(sdDataTable, dataTable);
        }

        internal void DropColumn(DataTable dataTable, string columnName)
        {
            var sdDataTable = _efContext.SDDataTables.Single(c => c.Name == dataTable.TableName);
            var column = _efContext.SDColumns.Single(c => c.SDDataTableId == sdDataTable.Id && c.Name == columnName);

            _dbTableRepository.RemoveColumn(dataTable.TableName, column);
            _efContext.SDColumns.Remove(column);
            _efContext.SaveChanges();

            dataTable.Columns.Remove(columnName);
        }

        internal void SyncSchema()
        {
            _syncService.Synchronize();
        }

        internal void AddTable(AddTableViewModel addTableViewModel)
        {
            string tableId = Guid.NewGuid().ToString();
            var sdTable = new SDDataTable(
                tableId,
                addTableViewModel.Name, true,
                addTableViewModel.ColumnViewModels.Select(c => new SDColumn(Guid.NewGuid().ToString(), c.Name, c.DataType, true, tableId)).ToList()
                );

            _dbTableRepository.Add(sdTable);
            _efContext.SDDataTables.Add(sdTable);
            _efContext.SaveChanges();

            var table = _dbTableRepository.List(sdTable);
            _view.AddGrid(table);
        }
    }
}
