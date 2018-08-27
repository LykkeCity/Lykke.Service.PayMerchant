using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lykke.Service.PayMerchant.Core.Domain;
using LykkePay.Common.Validation;

namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// Merchant group update details
    /// </summary>
    public class UpdateMerchantGroupModel
    {
        /// <summary>
        /// Gets or sets group id
        /// </summary>
        [Required]
        [RowKey(ErrorMessage = "Invalid characters used or value is empty")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets list of merchants
        /// </summary>
        [Required]
        [RowKey(ErrorMessage = "Invalid characters used or value is empty")]
        [NotEmptyCollection(ErrorMessage = "The collection contains no elements")]
        public IEnumerable<string> Merchants { get; set; }

        /// <summary>
        /// Gets or sets merchant group use
        /// </summary>
        [Required]
        [EnumDataType(typeof(MerchantGroupUse), ErrorMessage = "Invalid value, possible values are: Supervising, Billing")]
        public MerchantGroupUse? MerchantGroupUse { get; set; }
    }
}
