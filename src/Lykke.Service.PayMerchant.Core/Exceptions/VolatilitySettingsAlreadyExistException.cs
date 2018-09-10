using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PayMerchant.Core.Exceptions
{
    public class VolatilitySettingsAlreadyExistException : Exception
    {
        public VolatilitySettingsAlreadyExistException()
        {
        }

        public VolatilitySettingsAlreadyExistException(string merchantId) : base("Merchant volatility settings already exist")
        {
            MerchantId = merchantId;
        }

        public VolatilitySettingsAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VolatilitySettingsAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string MerchantId { get; set; }
    }
}
