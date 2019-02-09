using EFTest.Data;
using EFTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Repository
{
    class DbTableRepository
    {
        private const string TableName = nameof(SDDataTable);
        private readonly AdoContext _context;

        public DbTableRepository(AdoContext context)
        {
            _context = context;
        }

        public void Add(SDDataTable table)
        {
            var columnDefinitions = table.Columns
                .Select(c => $"{c.Name} {c.GetSqlType()}")
                .Aggregate((current, next) => $"{current}, {next}");

            var query = $"CREATE TABLE {table.Name}({columnDefinitions})";


            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void AddColumn(string tableName, SDColumn column)
        {
            var query = $"ALTER TABLE {tableName} ADD COLUMN {column.Name} {column.GetSqlType()}";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Remove(SDDataTable table)
        {

            var query = $"DROP TABLE {table.Name}";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public DataTable List(SDDataTable sDDataTable)
        {
            var query = $"SELECT * from {sDDataTable.Name}";

            using (var connection = _context.GetConnection())
            using (var adapter = new SQLiteDataAdapter(query, connection))
            {
                var table = new DataTable(sDDataTable.Name);
                adapter.Fill(table);
                return table;
            }
        }

        public void UpdateDataTable(SDDataTable sDDataTable, DataTable dataTable)
        {
            var query = $"SELECT * from {sDDataTable.Name}";

            using (var connection = _context.GetConnection())
            using (var adapter = new SQLiteDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }
        }

        public ICollection<DataTable> List(IEnumerable<SDDataTable> sDDataTables)
        {
            var tables = new List<DataTable>();

            foreach (var sdDataTable in sDDataTables)
            {
                tables.Add(List(sdDataTable));
            }
            return tables;
        }
    }
}
