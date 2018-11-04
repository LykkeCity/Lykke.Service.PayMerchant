using System.ComponentModel.DataAnnotations;
using LykkePay.Common.Validation;

namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// Update volatility settings model
    /// </summary>
    public class UpdateVolatilitySettingsModel
    {
        /// <summary>
        /// Gets or sets merchant id
        /// </summary>
        [Required]
        [RowKey]
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets zero coverage asset pairs, semicolon separated
        /// </summary>
        [Required]
        public string ZeroCoverageAssetPairs { get; set; }


        /// <summary>
        /// Gets or sets flag whether delta spread is fixed for the merchant or not 
        /// </summary>
        [Required]
        public bool IsDeltaSpreadFixed { get; set; }
    }
}
