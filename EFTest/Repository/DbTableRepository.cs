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
    enum ColumnAddState
    {
        Added,
        DuplicateWithoutConflict,
        Conflict
    }

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
            return $"{Name} {Type} {(string.IsNullOrEmpty(Default) ? "" : $"DEFAULT ({Default})")} {(IsPrimaryKey ? "PRIMARY KEY" : "")} {(NotNull ? "NOT NULL" : "")}";
        }

        public Type GetCSharpType()
        {
            if (Type == "INTEGER")
            {
                return typeof(int);
            }
            else if (Type == "DOUBLE")
            {
                return typeof(double);
            }
            else if (Type == "BOOLEAN")
            {
                return typeof(bool);
            }
            else if (Type == "DATETIME")
            {
                return typeof(DateTime);
            }
            else
            {
                return typeof(string);
            }
        }
    }

    class DbTableRepository
    {
        private const string TableName = nameof(SDDataTable);
        private readonly AdoContext _context;
        private string[] _defaultColumns = new[] { "Id TEXT DEFAULT (HEX(RANDOMBLOB(16))) PRIMARY KEY" };

        public DbTableRepository(AdoContext context)
        {
            _context = context;
        }

        public void Add(SDDataTable table)
        {
            var columnDefinitions = _defaultColumns
                .Concat(
                    table.Columns
                    .Select(c => $"{c.Name} {c.GetSqlType()}")
                )
                .Aggregate((current, next) => $"{current}, {next}");

            var query = $"CREATE TABLE {table.Name}({columnDefinitions})";


            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }


        public bool TryCreateEmptyTable(SDDataTable table)
        {
            var columnDefinitions = _defaultColumns
                .Aggregate((current, next) => $"{current}, {next}");

            var query = $"CREATE TABLE {table.Name}({columnDefinitions})";

            if (_context.TableExists(table.Name))
            {
                return false;
            }
            else
            {
                using (var connection = _context.GetConnection())
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    return true;
                }
            }

        }

        public ColumnAddState TryAddColumn(string tableName, SDColumn column)
        {
            if (!_context.TableExists(tableName))
            {
                throw new ArgumentException("table " + tableName + " does not exist");
            }

            var columns = GetColumns(tableName);
            var columnAlreadyExists = columns.Any(c => c.Name.Equals(column.Name, StringComparison.OrdinalIgnoreCase));

            if (!columnAlreadyExists)
            {
                var query = $"ALTER TABLE {tableName} ADD COLUMN {column.Name} {column.GetSqlType()}";

                using (var connection = _context.GetConnection())
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                return ColumnAddState.Added;
            }
            else
            {
                var duplicateColumn = columns.Single(c => c.Name == column.Name);
                if (duplicateColumn.GetCSharpType() == Type.GetType(column.DataType))
                {
                    return ColumnAddState.DuplicateWithoutConflict;
                }
                else
                {
                    return ColumnAddState.Conflict;
                }
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

        public void RemoveColumn(string tableName, SDColumn column)
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
