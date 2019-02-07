using EFTest.Data;
using EFTest.Models;
using System;
using System.Collections.Generic;
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

            var query = $"CREATE TABLE {TableName}({columnDefinitions})";


            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void AddColumn(SDColumn column)
        {
            var query = $"ALTER TABLE {TableName} ADD COLUMN {column.Name} {column.GetSqlType()}";

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
    }
}
