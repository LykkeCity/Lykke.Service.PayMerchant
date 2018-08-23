using System.ComponentModel.DataAnnotations;
using LykkePay.Common.Validation;

namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// Create merchant request details
    /// </summary>
    public class CreateMerchantRequest
    {
        /// <summary>
        /// Gets or sets merchant name
        /// </summary>
        [Required]
        [RowKey]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets merchant display name
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets merchant api key
        /// </summary>
        [Required]
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets merchant Lykke wallet id
        /// </summary>
        public string LwId { get; set; }
    }
}
