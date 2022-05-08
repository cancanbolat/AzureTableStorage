using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.Services
{
    public class TableStorage<T> : INoSQLStorage<T> where T : TableEntity, new()
    {
        CloudTableClient CloudTableClient;
        CloudTable CloudTable;

        public TableStorage()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=cancanbolatstorage;AccountKey=4BFh+H9kx+iV/X2oA2Bno0ZZz4GdAx4OkLAk2uNwTc1ioNagx7d23SpTJPcbEE0d9dsqu+UToLyyY47JPyHlag==;EndpointSuffix=core.windows.net");
            CloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable = CloudTableClient.GetTableReference(typeof(T).Name);
            CloudTable.CreateIfNotExists();
        }

        public async Task<T> Add(T entity)
        {
            TableOperation operation = TableOperation.InsertOrMerge(entity);
            return (await CloudTable.ExecuteAsync(operation)).Result as T;
        }

        public IQueryable<T> All() => CloudTable.CreateQuery<T>().AsQueryable();

        public async Task<T> Delete(string rowKey, string partitionKey)
        {
            TableOperation operation = TableOperation.Delete(await Get(rowKey, partitionKey));
            return (await CloudTable.ExecuteAsync(operation)).Result as T;
        }

        public async Task<T> Get(string rowKey, string partitionKey)
        {
            TableOperation operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            return (await CloudTable.ExecuteAsync(operation)).Result as T;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> query) => CloudTable.CreateQuery<T>().Where(query);

        public async Task<T> Update(T entity)
        {
            TableOperation operation = TableOperation.Replace(entity);
            return (await CloudTable.ExecuteAsync(operation)).Result as T;
        }
    }
}
