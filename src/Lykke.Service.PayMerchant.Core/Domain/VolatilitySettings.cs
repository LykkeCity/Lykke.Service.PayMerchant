namespace Lykke.Service.PayMerchant.Core.Domain
{
    public class VolatilitySettings : IVolatilitySettings
    {
        public string MerchantId { get; set; }
        public string ZeroCoverageAssetPairs { get; set; }
    }
}
