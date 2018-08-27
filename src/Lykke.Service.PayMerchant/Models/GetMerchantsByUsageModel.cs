﻿using System.ComponentModel.DataAnnotations;
using Lykke.Service.PayMerchant.Core.Domain;
using LykkePay.Common.Validation;

namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// Get merchants by usage request details
    /// </summary>
    public class GetMerchantsByUsageModel
    {
        /// <summary>
        /// Gets or sets merchant id
        /// </summary>
        [Required]
        [RowKey(ErrorMessage = "Invalid characters used or value is empty")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets merchant group use
        /// </summary>
        [Required]
        [EnumDataType(typeof(MerchantGroupUse), ErrorMessage = "Invalid value, possible values are: Supervising, Billing")]
        public MerchantGroupUse? MerchantGroupUse { get; set; }
    }
}
