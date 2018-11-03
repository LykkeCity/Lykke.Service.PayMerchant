using System;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using JetBrains.Annotations;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    public class VolatilitySettingsRepository : IVolatilitySettingsRepository
    {
        private readonly INoSQLTableStorage<VolatilitySettingsEntity> _storage;

        public VolatilitySettingsRepository(
            [NotNull] INoSQLTableStorage<VolatilitySettingsEntity> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async Task<IVolatilitySettings> AddAsync(IVolatilitySettings src)
        {
            var entity = VolatilitySettingsEntity.ByMerchant.Create(src);

            await _storage.InsertThrowConflict(entity);

            return Mapper.Map<VolatilitySettings>(entity);
        }

        public async Task<IVolatilitySettings> GetAsync(string merchantId)
        {
            VolatilitySettingsEntity entity = await _storage.GetDataAsync(
                VolatilitySettingsEntity.ByMerchant.GeneratePartitionKey(merchantId),
                VolatilitySettingsEntity.ByMerchant.GenerateRowKey(merchantId));

            if (entity == null)
                throw new KeyNotFoundException();

            return Mapper.Map<VolatilitySettings>(entity);
        }

        public async Task<IVolatilitySettings> UpdateAsync(IVolatilitySettings src)
        {
            VolatilitySettingsEntity updatedEntity = await _storage.MergeAsync(
                VolatilitySettingsEntity.ByMerchant.GeneratePartitionKey(src.MerchantId),
                VolatilitySettingsEntity.ByMerchant.GenerateRowKey(src.MerchantId), entity =>
                {
                    if (!string.IsNullOrEmpty(src.ZeroCoverageAssetPairs))
                        entity.ZeroCoverageAssetPairs = src.ZeroCoverageAssetPairs;

                    entity.IsDeltaSpreadFixed = src.IsDeltaSpreadFixed;

                    return entity;
                });

            if (updatedEntity == null)
                throw new KeyNotFoundException();

            return Mapper.Map<VolatilitySettings>(updatedEntity);
        }

        public async Task DeleteAsync(string merchantId)
        {
            VolatilitySettingsEntity deletedEntity = await _storage.DeleteAsync(
                VolatilitySettingsEntity.ByMerchant.GeneratePartitionKey(merchantId),
                VolatilitySettingsEntity.ByMerchant.GenerateRowKey(merchantId));

            if (deletedEntity == null)
                throw new KeyNotFoundException();
        }
    }
}
