using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
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

        public SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection("Data Source=db.sqlite");
            connection.Open();
            return connection;
        }

        public void ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            using (var command = new SqliteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
