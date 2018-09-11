using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using AzureStorage.Tables.Templates.Index;
using JetBrains.Annotations;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly INoSQLTableStorage<MerchantEntity> _storage;
        private readonly INoSQLTableStorage<AzureIndex> _emailIndexStorage;

        public MerchantRepository(
            [NotNull] INoSQLTableStorage<MerchantEntity> storage, 
            [NotNull] INoSQLTableStorage<AzureIndex> emailIndexStorage)
        {
            _storage = storage;
            _emailIndexStorage = emailIndexStorage;
        }

        public async Task<IReadOnlyList<IMerchant>> GetAsync()
        {
            string emailIndexRowKey = MerchantEntity.IndexByEmail.GenerateRowKey();

            IEnumerable<MerchantEntity> entities = await _storage.GetDataAsync(e => e.RowKey != emailIndexRowKey);

            return entities.ToList();
        }

        public async Task<IMerchant> GetAsync(string merchantName)
        {
            return await _storage.GetDataAsync(
                MerchantEntity.ById.GeneratePartitionKey(merchantName), 
                MerchantEntity.ById.GenerateRowKey(merchantName));
        }

        public async Task<IReadOnlyList<IMerchant>> FindApiKeyAsync(string apiKey)
        {
            IList<MerchantEntity> entities = await _storage.GetDataAsync(merchant => merchant.ApiKey == apiKey);

            return entities.ToList();
        }

        public async Task<IMerchant> FindEmailAsync(string email)
        {
            AzureIndex index = await _emailIndexStorage.GetDataAsync(
                MerchantEntity.IndexByEmail.GeneratePartitionKey(email),
                MerchantEntity.IndexByEmail.GenerateRowKey());

            MerchantEntity entity = await _storage.GetDataAsync(index);

            return Mapper.Map<Merchant>(entity);
        }

        public async Task<IMerchant> InsertAsync(IMerchant merchant)
        {
            var existingEmail = await _emailIndexStorage.GetDataAsync(
                MerchantEntity.IndexByEmail.GeneratePartitionKey(merchant.Email),
                MerchantEntity.IndexByEmail.GenerateRowKey());

            if (existingEmail != null)
                throw new DuplicateMerchantEmailException(merchant.Email);

            var entity = MerchantEntity.ById.Create(merchant);
            
            try
            {
                await _storage.InsertThrowConflict(entity);
            }
            catch (DuplicateKeyException)
            {
                throw new DuplicateMerchantNameException(merchant.Name);
            }

            var index = MerchantEntity.IndexByEmail.Create(entity);

            await _emailIndexStorage.InsertThrowConflict(index);

            return entity;
        }

        public async Task ReplaceAsync(IMerchant merchant)
        {
            var entity = new MerchantEntity(
                MerchantEntity.ById.GeneratePartitionKey(merchant.Name),
                MerchantEntity.ById.GenerateRowKey(merchant.Name));

            Mapper.Map(merchant, entity);

            entity.ETag = "*";

            await _storage.ReplaceAsync(entity);
        }

        public async Task DeleteAsync(string merchantName)
        {
            MerchantEntity merchantEntity = await _storage.DeleteAsync(
                MerchantEntity.ById.GeneratePartitionKey(merchantName),
                MerchantEntity.ById.GenerateRowKey(merchantName));

            if (merchantEntity == null)
                throw new MerchantNotFoundException(merchantName);

            await _emailIndexStorage.DeleteAsync(
                MerchantEntity.IndexByEmail.GeneratePartitionKey(merchantEntity.Email),
                MerchantEntity.IndexByEmail.GenerateRowKey());
        }
    }
}
