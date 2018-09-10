namespace Lykke.Service.PayMerchant.Client.Models
{
    /// <summary>
    /// Update volatility settings model
    /// </summary>
    public class UpdateVolatilitySettingsRequest
    {
        /// <summary>
        /// Gets or sets merchant id
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets zero coverage asset paits, semicolon separated
        /// </summary>
        public string ZeroCoverageAssetPairs { get; set; }
    }
}
