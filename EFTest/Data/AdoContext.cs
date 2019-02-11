using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Data
{
    class AdoContext
    {
        public AdoContext()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection("Data Source=db.sqlite");
            connection.Open();
            return connection;
        }

        public void ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                return command.ExecuteScalar();
            }
        }

        public bool TableExists(string tableName)
        {
            var query = $"SELECT count(*) FROM sqlite_master WHERE type='table' and UPPER(name) = UPPER(@name)";

            using (var conneciton = GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            {
                command.Parameters.AddWithValue("@name", tableName);
                var tableExists = Convert.ToBoolean(command.ExecuteScalar());
                return tableExists;
            }
        }
    }
}
