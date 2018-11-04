using System;
using System.Linq;
using System.Security.Cryptography;
using Common;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.PayMerchant.Core.Domain;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateIfDirty)]
    public class VolatilitySettingsEntity : AzureTableEntity
    {
        private const int PartitionKeyLength = 3;

        private bool _isDeltaSpreadFixed;

        public string MerchantId { get; set; }

        public string ZeroCoverageAssetPairs { get; set; }

        public bool IsDeltaSpreadFixed
        {
            get => _isDeltaSpreadFixed;
            set
            {
                if (_isDeltaSpreadFixed != value)
                {
                    _isDeltaSpreadFixed = value;
                    MarkValueTypePropertyAsDirty(nameof(IsDeltaSpreadFixed));
                }
            }
        } 

        public static class ByMerchant
        {
            public static string GeneratePartitionKey(string merchantId)
            {
                string hash = Convert
                    .ToBase64String(SHA1.Create().ComputeHash(merchantId.ToUtf8Bytes()))
                    .Replace('/', '_');

                return new string(hash.Take(PartitionKeyLength).ToArray());
            }

            public static string GenerateRowKey(string merchantId)
            {
                return merchantId;
            }

            public static VolatilitySettingsEntity Create(IVolatilitySettings src)
            {
                return new VolatilitySettingsEntity
                {
                    PartitionKey = GeneratePartitionKey(src.MerchantId),
                    RowKey = GenerateRowKey(src.MerchantId),
                    MerchantId = src.MerchantId,
                    ZeroCoverageAssetPairs = src.ZeroCoverageAssetPairs,
                    IsDeltaSpreadFixed = src.IsDeltaSpreadFixed
                };
            }
        }
    }
}
