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
    class SqlColumn
    {
        public SqlColumn(string name, string type, bool notNull, string @default, bool isPrimaryKey)
        {
            Name = name;
            Type = type;
            NotNull = notNull;
            Default = @default;
            IsPrimaryKey = isPrimaryKey;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public bool NotNull { get; set; }
        public string Default { get; set; }
        public bool IsPrimaryKey { get; set; }

        public string GetSqlString()
        {
            return $"{Name} {Type} {(string.IsNullOrEmpty(Default) ? "" : $"DEFAULT ({Default})")} {(IsPrimaryKey ? "PRIMARY KEY" : "")} {(NotNull ? "NOT NULL": "")}";
        }
    }

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

        public ICollection<SqlColumn> GetColumns(string tableName)
        {
            var columns = new List<SqlColumn>();
            var query = $"PRAGMA table_info('{tableName}')";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    columns.Add(new SqlColumn(
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetBoolean(3),
                        (reader[4] == DBNull.Value ? null : reader.GetString(4)),
                        reader.GetBoolean(5)));
                }
            }
            return columns;
        }

        public void DropColumn(string tableName, SDColumn column)
        {
            var tempTable = $"_{tableName}";
            var columns = GetColumns(tableName).Where(c => c.Name != column.Name);
            var columnsString = string.Join(", ", columns.Select(c => c.GetSqlString()));



            var renameQuery = $"ALTER TABLE {tableName} RENAME TO {tempTable}";
            var createQuery = $"CREATE TABLE {tableName}({columnsString})";
            var insertQuery = $"INSERT INTO {tableName} SELECT {string.Join(", ", columns.Select(c => c.Name))} FROM {tempTable}";
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
