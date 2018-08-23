using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Common;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly INoSQLTableStorage<MerchantEntity> _storage;

        private const int PartitionKeyLength = 3;

        public MerchantRepository(
            INoSQLTableStorage<MerchantEntity> storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyList<IMerchant>> GetAsync()
        {
            IEnumerable<MerchantEntity> entities = await _storage.GetDataAsync();

            return entities.ToList();
        }

        public async Task<IMerchant> GetAsync(string merchantName)
        {
            return await _storage.GetDataAsync(GetPartitionKey(merchantName), GetRowKey(merchantName));
        }

        public async Task<IReadOnlyList<IMerchant>> FindAsync(string apiKey)
        {
            IList<MerchantEntity> entities = await _storage.GetDataAsync(merchant => merchant.ApiKey == apiKey);

            return entities.ToList();
        }

        public async Task<IMerchant> InsertAsync(IMerchant merchant)
        {
            var entity = new MerchantEntity(GetPartitionKey(merchant.Name), GetRowKey(merchant.Name));

            Mapper.Map(merchant, entity);
            
            try
            {
                await _storage.InsertThrowConflict(entity);
            }
            catch (DuplicateKeyException)
            {
                throw new DuplicateMerchantNameException(merchant.Name);
            }

            return entity;
        }

        public async Task ReplaceAsync(IMerchant merchant)
        {
            var entity = new MerchantEntity(GetPartitionKey(merchant.Name), GetRowKey(merchant.Name));

            Mapper.Map(merchant, entity);

            entity.ETag = "*";

            await _storage.ReplaceAsync(entity);
        }

        public async Task DeleteAsync(string merchantName)
        {
            MerchantEntity merchantEntity =
                await _storage.DeleteAsync(GetPartitionKey(merchantName), GetRowKey(merchantName));

            if (merchantEntity == null)
                throw new MerchantNotFoundException(merchantName);
        }

        private static string GetPartitionKey(string merchantName)
        {
            string hash = Convert
                .ToBase64String(SHA1.Create().ComputeHash(merchantName.ToUtf8Bytes()))
                .Replace('/', '_');

            if (!hash.IsValidPartitionOrRowKey())
                throw new InvalidRowKeyValueException(nameof(hash), hash);

            return new string(hash.Take(PartitionKeyLength).ToArray());
        }

        private static string GetRowKey(string merchantName)
        {
            if (!merchantName.IsValidPartitionOrRowKey())
                throw new InvalidRowKeyValueException(nameof(merchantName), merchantName);

            return merchantName;
        }
    }
}
