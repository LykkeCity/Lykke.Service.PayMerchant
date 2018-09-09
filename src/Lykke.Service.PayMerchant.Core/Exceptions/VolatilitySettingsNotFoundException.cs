using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PayMerchant.Core.Exceptions
{
    public class VolatilitySettingsNotFoundException : Exception
    {
        public VolatilitySettingsNotFoundException()
        {
        }

        public VolatilitySettingsNotFoundException(string merchantId) : base("Merchant volatility settigs not found")
        {
            MerchantId = merchantId;
        }

        public VolatilitySettingsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VolatilitySettingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string MerchantId { get; set; }
    }
}
