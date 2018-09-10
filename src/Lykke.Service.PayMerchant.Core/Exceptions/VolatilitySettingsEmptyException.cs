using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PayMerchant.Core.Exceptions
{
    public class VolatilitySettingsEmptyException : Exception
    {
        public VolatilitySettingsEmptyException()
        {
        }

        public VolatilitySettingsEmptyException(string merchantId) : base("Merchant volatility settings are empty")
        {
            MerchantId = merchantId;
        }

        public VolatilitySettingsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VolatilitySettingsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string MerchantId { get; set; }
    }
}
