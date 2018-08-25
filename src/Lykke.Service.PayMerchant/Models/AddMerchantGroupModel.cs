﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lykke.Service.PayMerchant.Core.Domain;
using LykkePay.Common.Validation;

namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// New merchant gtoup details
    /// </summary>
    public class AddMerchantGroupModel
    {
        /// <summary>
        /// Gets or sets display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets group owner
        /// </summary>
        [Required]
        [RowKey(ErrorMessage = "Invalid characters used or value is empty")]
        public string OwnerId { get; set; }

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
