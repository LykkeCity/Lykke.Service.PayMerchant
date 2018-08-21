using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PayMerchant.Client 
{
    /// <summary>
    /// PayMerchant client settings.
    /// </summary>
    public class PayMerchantServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
