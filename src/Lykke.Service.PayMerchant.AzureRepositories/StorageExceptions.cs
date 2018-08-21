using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.PayMerchant.Core.Exceptions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.Protocol;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    public static class StorageExceptions
    {
        public static async Task InsertThrowConflict<T>(this INoSQLTableStorage<T> storage, T entity) where T : ITableEntity, new()
        {
            const int conflict = 409;

            try
            {
                await storage.InsertAsync(entity, conflict);
            }
            catch (StorageException exception)
            {
                if (exception.RequestInformation != null &&
                    exception.RequestInformation.HttpStatusCode == conflict &&
                    exception.RequestInformation.ExtendedErrorInformation.ErrorCode == TableErrorCodeStrings.EntityAlreadyExists)
                {
                    throw new DuplicateKeyException("Entity already exists", exception);
                }

                throw;
            }
        }
    }
}
