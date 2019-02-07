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
    class DbColumnRepository
    {
        private const string TableName = nameof(SDColumn);
        private readonly AdoContext _context;

        public DbColumnRepository(AdoContext context)
        {
            _context = context;
        }

        public void Add(SDColumn column)
        {
            column.Id = Guid.NewGuid().ToString();

            var query = $@"
INSERT INTO {TableName}(id, name, datatype, comboboxvalues, synchronize, sddatatableid) 
VALUES (@id, @name, @datatype, @comboboxvalues @synchronize, @sddatatableid)";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", column.Id);
                command.Parameters.AddWithValue("@name", column.Name);
                command.Parameters.AddWithValue("@datatype", column.DataType);

                if (column is SDComboboxColumn sDComboboxColumn)
                {
                    command.Parameters.AddWithValue("@comboboxvalues", sDComboboxColumn.ComboboxValues);
                }
                else
                {
                    command.Parameters.AddWithValue("@comboboxvalues", DBNull.Value);
                }

                command.Parameters.AddWithValue("@synchronize", column.Synchronize);
                command.Parameters.AddWithValue("@sddatatableid", column.SDDataTableId);

                command.ExecuteNonQuery();
            }
        }

        public void Remove(SDColumn column)
        {
            column.Id = Guid.NewGuid().ToString();

            var query = $@"DELETE FROM {TableName} WHERE id=@id";

            using (var connection = _context.GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", column.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
