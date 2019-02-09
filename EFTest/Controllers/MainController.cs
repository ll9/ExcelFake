using EFTest.Data;
using EFTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Controllers
{
    class MainController
    {
        private MainForm _view;
        private ApplicationDbContext _efContext;
        private AdoContext _adoContext;
        private DbTableRepository _dbTableRepository;

        public MainController(MainForm form1)
        {
            _view = form1;
            _efContext = new ApplicationDbContext();
            _adoContext = new AdoContext();
            _dbTableRepository = new DbTableRepository(_adoContext);
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
    }
}
