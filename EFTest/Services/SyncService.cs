using EFTest.Controllers;
using EFTest.Data;
using EFTest.Extensions;
using EFTest.Models;
using EFTest.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Services
{
    class SyncService
    {
        private const string BaseUrl = "https://localhost:44331";
        private const string ApiUrl = "api/SDSchemaObject";
        private readonly MainController _controller;
        private readonly ApplicationDbContext _efContext;
        private readonly DbTableRepository _dbTableRepository;
        private RestClient _client;

        public SyncService(MainController controller, ApplicationDbContext efContext, DbTableRepository dbTableRepository)
        {
            _client = new RestClient(BaseUrl);
            _controller = controller;
            _efContext = efContext;
            _dbTableRepository = dbTableRepository;
        }

        public async void Synchronize()
        {
            await DownloadChanges();
            await UploadChanges();
        }

        public async Task DownloadChanges()
        {
            var request = new RestRequest(ApiUrl, Method.GET);
            request.AddHeader("Authorization", "Bearer token");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = _client.Execute<SDSchemaObject>(request);
            var sDSchema = JsonConvert.DeserializeObject<SDSchemaObject>(response.Content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("OK");
            }
            else
            {
                throw new Exception("Network error");
            }

            CopyNewReomteEntriesToLocal(sDSchema);
            RemoveDeletedRemoteEntriesToLocal(sDSchema);
        }

        public async Task UploadChanges()
        {
            await CopyNewLocalEntriesToRemote();
            await RemoveDeletedLocalEntriesToRemote();
        }

        private void CopyNewReomteEntriesToLocal(SDSchemaObject data)
        {
            var newTables = data.SDDataTables
                .Where(remote => !_efContext.SDStatuses.Any(local => local.Id == remote.Id))
                .ToList();

            var newColumns = data.SDDataTables
                .SelectMany(t => t.Columns)
                .Where(remote => !_efContext.SDStatuses.Any(local => local.Id == remote.Id))
                .ToList();

            foreach (var table in newTables)
            {
                // Remove Navigation Property in order to take care of it manually later
                table.Columns = null;

                var createdEmptyTable = _dbTableRepository.TryCreateEmptyTable(table);
                if (createdEmptyTable)
                {
                    _efContext.SDDataTables.Add(table);
                }
                _efContext.SDStatuses.Add(new SDStatus(table.Id));
            }
            foreach (var column in newColumns)
            {
                var tableName = _efContext.SDDataTables.Single(t => t.Id == column.SDDataTableId).Name;

                var result = _dbTableRepository.TryAddColumn(tableName, column);
                if (result == ColumnAddState.Added)
                {
                    _efContext.SDColumns.Add(column);
                    _efContext.SDStatuses.Add(new SDStatus(column.Id));
                }
                else if (result == ColumnAddState.DuplicateWithoutConflict)
                {
                    _efContext.SDStatuses.Add(new SDStatus(column.Id));
                }
                else
                {
                    throw new Exception("Column Conflict");
                }
                _efContext.SaveChanges();

            }
            _efContext.SaveChanges();
        }

        private void RemoveDeletedRemoteEntriesToLocal(SDSchemaObject data)
        {
            var removedTables = _efContext.SDStatuses
                .Where(s => !data.SDDataTables.Any(t => t.Id == s.Id))
                .Select(s => _efContext.SDDataTables.SingleOrDefault(t => t.Id == s.Id))
                .ToList();
            removedTables.RemoveAll(i => i == null);

            var removedColumns = data.SDColumns
                .Where(s => !data.SDColumns.Any(c => c.Id == s.Id))
                .Select(s => _efContext.SDColumns.SingleOrDefault(c => c.Id == s.Id))
                .ToList();
            removedColumns.RemoveAll(i => i == null);

            foreach (var table in removedTables)
            {
                _dbTableRepository.Remove(table);
                _efContext.SDDataTables.Remove(table);
                _efContext.SDStatuses.Remove(new SDStatus(table.Id));
                _efContext.SDStatuses.RemoveRange(table.Columns.Select(c => new SDStatus(c.Id)));
            }
            foreach (var column in removedColumns)
            {
                var tableName = _efContext.SDDataTables.Single(t => t.Id == column.SDDataTableId).Name;
                _dbTableRepository.RemoveColumn(tableName, column);
                _efContext.SDColumns.Remove(column);
                _efContext.SDStatuses.Remove(new SDStatus(column.Id));
            }
            _efContext.SaveChanges();
        }

        public async Task CopyNewLocalEntriesToRemote()
        {
            var newTables = _efContext.SDDataTables
                .Include(t => t.Columns)
                .Where(table => !_efContext.SDStatuses.Any(s => s.Id == table.Id))
                .ToList();

            var newColumns = _efContext.SDColumns
                .Where(column => !_efContext.SDStatuses.Any(s => s.Id == column.Id))
                .Where(column => !newTables.Any(t => t.Columns.Any(c => c.Id != column.SDDataTableId)))
                .ToList();

            var schemaObject = new SDSchemaObject(newTables, newColumns);


            var request = new RestRequest(ApiUrl, Method.POST);
            request.AddHeader("Authorization", "Bearer token");
            request.AddJsonBody(schemaObject);

            var response = _client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Console.WriteLine("OK");
            }
            else
            {
                throw new Exception("Network error");
            }


            foreach (var table in schemaObject.SDDataTables)
            {
                _efContext.SDStatuses.Add(new SDStatus(table.Id));
                foreach (var column in table.Columns)
                {
                    _efContext.SDStatuses.Add(new SDStatus(column.Id));
                }
            }
            foreach (var column in schemaObject.SDColumns)
            {
                _efContext.SDStatuses.Add(new SDStatus(column.Id));
            }
            _efContext.SaveChanges();
        }

        public async Task RemoveDeletedLocalEntriesToRemote()
        {
            var deletedStatuses = _efContext.SDStatuses
                .Where(s => !_efContext.SDDataTables.Any(t => t.Id == s.Id) &&
                            !_efContext.SDColumns.Any(c => c.Id == s.Id))
                .ToList();


            var request = new RestRequest(ApiUrl, Method.DELETE);
            request.AddHeader("Authorization", "Bearer token");
            request.AddJsonBody(deletedStatuses.Select(s => s.Id));

            var response = _client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("OK");
            }
            else
            {
                throw new Exception("Network error");
            }

            foreach (var status in deletedStatuses)
            {
                _efContext.RemoveRange(deletedStatuses);
            }
            _efContext.SaveChanges();

        }
    }
}
