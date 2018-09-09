namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// Merchant volatility settings
    /// </summary>
    public class VolatilitySettingsResponse
    {
        /// <summary>
        /// Gets or sets merchant id
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets zero volatility coverage asset pairs, semicolon separated
        /// </summary>
        public string ZeroCoverageAssetPairs { get; set; }
    }
}
