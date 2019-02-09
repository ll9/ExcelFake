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

        public ICollection<string> GetColumns(string tableName)
        {
            var columns = new List<string>();
            var query = $"PRAGMA table_info('{tableName}')";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    columns.Add(reader.GetString(1));
                }
            }
            return columns;
        }

        public void DropColumn(string tableName, SDColumn column)
        {
            var tempTable = $"_{tableName}";
            var metaQuery = $"SELECT sql FROM sqlite_master WHERE type='table' and name='{tableName}'";
            var tableDefinition = _context.ExecuteScalar(metaQuery).ToString();

            var createStatement = tableDefinition.Split(new[] { '(' }, 2)[0];
            var columnStatement = tableDefinition.Split(new[] { '(' }, 2)[1].TrimEnd(')');
            var columns = columnStatement.Split('\n').Select(item => item.TrimEnd(',')).ToList();
            var droppedColumn = columns.Single(c => c.StartsWith($"\"{column.Name}\""));
            columns.Remove(droppedColumn);
            var newColumns = string.Join(",", columns);

            var renameQuery = $"ALTER TABLE {tableName} RENAME TO {tempTable}";
            var createQuery = $"{createStatement}({newColumns})";
            var insertQuery = $"INSERT INTO {tableName} SELECT {string.Join(",", GetColumns(tableName))} FROM {tempTable}";
            var dropQuery = $"DROP TABLE {tempTable}";


            _context.ExecuteQuery(renameQuery);
            _context.ExecuteQuery(createQuery);
            _context.ExecuteQuery(insertQuery);
            _context.ExecuteQuery(dropQuery);
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
