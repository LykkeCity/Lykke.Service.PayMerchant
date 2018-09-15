using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PayMerchant.Core.Exceptions
{
    public class MerchantEmailUpdateException : Exception
    {
        public MerchantEmailUpdateException()
        {
        }

        public MerchantEmailUpdateException(string merchantId) : base("Existing email address can't be updated")
        {
            MerchantId = merchantId;
        }

        public MerchantEmailUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantEmailUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string MerchantId { get; set; }
    }
}
