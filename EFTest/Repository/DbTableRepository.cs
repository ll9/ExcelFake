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
            table.Id = Guid.NewGuid().ToString();

            var query = $"INSERT INTO {TableName}(id, name, synchronize) VALUES (@id, @name, @synchronize)";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", table.Id);
                command.Parameters.AddWithValue("@name", table.Name);
                command.Parameters.AddWithValue("@synchronize", table.Synchronize);

                command.ExecuteNonQuery();
            }
        }

        public void Remove(SDDataTable table)
        {

            var query = $"DELETE FROM {TableName} WHERE id=@id";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", table.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
