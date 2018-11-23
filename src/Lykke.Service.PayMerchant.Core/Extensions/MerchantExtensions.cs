using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.PayMerchant.Core.Domain;

namespace Lykke.Service.PayMerchant.Core.Extensions
{
    public static class MerchantExtensions
    {
        public static void TrimProperties(this IMerchant merchant)
        {
            merchant.Name = merchant.Name.Trim();
            merchant.DisplayName = merchant.DisplayName.Trim();
            merchant.Email = merchant.Email?.Trim();
        }
    }
}
