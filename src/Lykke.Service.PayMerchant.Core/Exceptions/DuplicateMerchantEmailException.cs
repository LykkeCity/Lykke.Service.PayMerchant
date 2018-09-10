using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PayMerchant.Core.Exceptions
{
    public class DuplicateMerchantEmailException : Exception
    {
        public DuplicateMerchantEmailException()
        {
        }

        public DuplicateMerchantEmailException(string email) : base("Merchant with the same email already exists")
        {
            Email = email;
        }

        public DuplicateMerchantEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateMerchantEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Email { get; set; }
    }
}
