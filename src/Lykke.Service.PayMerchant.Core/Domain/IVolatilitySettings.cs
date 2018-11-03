namespace Lykke.Service.PayMerchant.Core.Domain
{
    public interface IVolatilitySettings
    {
        string MerchantId { get; set; }

        string ZeroCoverageAssetPairs { get; set; }

        bool IsDeltaSpreadFixed { get; set; }
    }
}
