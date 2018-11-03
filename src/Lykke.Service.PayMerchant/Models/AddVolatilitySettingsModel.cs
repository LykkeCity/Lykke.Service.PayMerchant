using System.ComponentModel.DataAnnotations;
using LykkePay.Common.Validation;

namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// New volatility settings model
    /// </summary>
    public class AddVolatilitySettingsModel
    {
        /// <summary>
        /// Gets or sets merchant id
        /// </summary>
        [Required]
        [RowKey]
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets zero coverage asset paits, semicolon separated
        /// </summary>
        [Required]
        public string ZeroCoverageAssetPairs { get; set; }

        /// <summary>
        /// Gets or sets flag whether delta spread if fixed for the merchant or not
        /// </summary>
        [Required]
        public bool IsDeltaSpreadFixed { get; set; }
    }
}
