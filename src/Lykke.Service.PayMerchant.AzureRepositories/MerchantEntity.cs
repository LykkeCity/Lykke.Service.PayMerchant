using System;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using AzureStorage.Tables.Templates.Index;
using Common;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateIfDirty)]
    public class MerchantEntity : AzureTableEntity, IMerchant
    {
        private const int PartitionKeyLength = 3;

        public MerchantEntity()
        {
        }

        public MerchantEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string Id => RowKey;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ApiKey { get; set; }

        public string LwId { get; set; }

        public string Email { get; set; }

        public static class ById
        {
            public static string GeneratePartitionKey(string merchantId)
            {
                string hash = Convert
                    .ToBase64String(SHA1.Create().ComputeHash(merchantId.ToUtf8Bytes()))
                    .Replace('/', '_');

                if (!hash.IsValidPartitionOrRowKey())
                    throw new InvalidRowKeyValueException(nameof(hash), hash);

                return new string(hash.Take(PartitionKeyLength).ToArray());
            }

            public static string GenerateRowKey(string merchantId)
            {
                if (!merchantId.IsValidPartitionOrRowKey())
                    throw new InvalidRowKeyValueException(nameof(merchantId), merchantId);

                return merchantId;
            }

            public static MerchantEntity Create(IMerchant src)
            {
                var entity = new MerchantEntity(GeneratePartitionKey(src.Name), GenerateRowKey(src.Name));

                Mapper.Map(src, entity);

                return entity;
            }
        }

        public static class IndexByEmail
        {
            public static string GeneratePartitionKey(string email)
            {
                return email;
            }

            public static string GenerateRowKey()
            {
                return "IndexByEmail";
            }

            public static AzureIndex Create(MerchantEntity entity)
            {
                return AzureIndex.Create(
                    GeneratePartitionKey(entity.Email),
                    GenerateRowKey(), entity);
            }
        }
    }
}
