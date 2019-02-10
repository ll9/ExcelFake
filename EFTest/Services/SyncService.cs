using EFTest.Controllers;
using EFTest.Data;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;
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
        private const string BaseUrl = "http://example.com";
        private readonly MainController _controller;
        private readonly ApplicationDbContext _efContext;
        private RestClient _client;

        public SyncService(MainController controller, ApplicationDbContext efContext)
        {
            _client = new RestClient(BaseUrl);
            _controller = controller;
            _efContext = efContext;
        }

        public async void Synchronize()
        {
            await DownloadChanges();
            await UploadChanges();
        }

        public async Task DownloadChanges()
        {
            var request = new RestRequest("api/sync-schema", Method.GET);
            request.AddHeader("Authorization", "Bearer token");

            //var response = _client.Execute<SDSchemaObject>(request);
            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Console.WriteLine("OK");
            //}
            //else
            //{
            //    throw new Exception("Network error");
            //}

            //CopyNewReomteEntriesToLocal(response.Data);
            //RemoveDeletedRemoteEntriesToLocal(response.Data);
        }

        public async Task UploadChanges()
        {
            await CopyNewLocalEntriesToRemote();
            await RemoveDeletedLocalEntriesToRemote();
        }

        private void CopyNewReomteEntriesToLocal(SDSchemaObject data)
        {
            var newTables = data.SDDataTables
                .Where(remote => _efContext.SDStatuses.Any(local => local.Id == remote.Id));

            var newColumns = data.SDColumns
                .Where(remote => _efContext.SDStatuses.Any(local => local.Id == remote.Id));

            //foreach (var table in newTables)
            //{
            //    _controller.AddTable(table);
            //}
            //foreach (var column in newColumns)
            //{
            //    _controller.AddColumn(column);
            //}
        }

        private void RemoveDeletedRemoteEntriesToLocal(SDSchemaObject data)
        {
            var removedTables = _efContext.SDStatuses
                .Where(s => !data.SDDataTables.Any(t => t.Id == s.Id))
                .ToList();


            var removedColumns = data.SDColumns
                .Where(s => !data.SDColumns.Any(c => c.Id == s.Id))
                .ToList();

            //foreach (var table in newTables)
            //{
            //    _controller.RemoveTable(table);
            //}
            //foreach (var column in newColumns)
            //{
            //    _controller.RemoveColumn(column);
            //}
        }

        public async Task CopyNewLocalEntriesToRemote()
        {
            var newTables = _efContext.SDDataTables
                .Include(t => t.Columns)
                .Where(table => !_efContext.SDStatuses.Any(s => s.Id == table.Id))
                .ToList();

            var newColumns = _efContext.SDColumns
                .Where(column => !_efContext.SDStatuses.Any(s => s.Id == column.Id))
                .Where(column => !newTables.Any(t => t.Columns.Any(c =>  c.Id != column.SDDataTableId)))
                .ToList();

            var schemaObject = new SDSchemaObject(newTables, newColumns);


            var request = new RestRequest("api/sync-schema", Method.POST);
            request.AddHeader("Authorization", "Bearer token");
            request.AddJsonBody(schemaObject);

            //var response = _client.Execute(request);
            //if (response.StatusCode == System.Net.HttpStatusCode.Created)
            //{
            //    Console.WriteLine("OK");
            //}
            //else
            //{
            //    throw new Exception("Network error");
            //}


            //foreach (var table in schemaObject.SDDataTables)
            //{
            //    _efContext.SDStatuses.Add(new SDStatus(table.Id));
            //    foreach (var column in table.Columns)
            //    {
            //        _efContext.SDStatuses.Add(new SDStatus(column.Id));
            //    }
            //}
            //foreach (var column in schemaObject.SDColumns)
            //{
            //    _efContext.SDStatuses.Add(new SDStatus(column.Id));
            //}
            //_efContext.SaveChanges();
        }

        public async Task RemoveDeletedLocalEntriesToRemote()
        {
            var deletedStatuses = _efContext.SDStatuses
                .Where(s => !_efContext.SDDataTables.Any(t => t.Id == s.Id) ||
                            !_efContext.SDColumns.Any(c => c.Id == s.Id))
                .ToList();


            var request = new RestRequest("api/sync-schema", Method.DELETE);
            request.AddHeader("Authorization", "Bearer token");
            request.AddJsonBody(deletedStatuses.Select(s => s.Id));

            //var response = _client.Execute(request);
            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Console.WriteLine("OK");
            //}
            //else
            //{
            //    throw new Exception("Network error");
            //}

            //foreach (var status in deletedStatuses)
            //{
            //    _efContext.RemoveRange(deletedStatuses);
            //}
            //_efContext.SaveChanges();

        }
    }
}
