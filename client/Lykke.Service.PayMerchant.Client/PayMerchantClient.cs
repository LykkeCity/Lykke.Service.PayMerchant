using Lykke.HttpClientGenerator;

namespace Lykke.Service.PayMerchant.Client
{
    /// <summary>
    /// PayMerchant API aggregating interface.
    /// </summary>
    public class PayMerchantClient : IPayMerchantClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>
        /// Interface to PayMerchant Api.
        /// </summary>
        public IPayMerchantApi Api { get; private set; }

        /// <summary>
        /// Interface to PayMerchant Groups Api
        /// </summary>
        public IPayMerchantGroupsApi Groups { get; private set; }

        /// <summary>
        /// Interface tp PayMerchant Settings Api
        /// </summary>
        public IPayMerchantSettingsApi Settings { get; set; }

        /// <summary>C-tor</summary>
        public PayMerchantClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IPayMerchantApi>();
            Groups = httpClientGenerator.Generate<IPayMerchantGroupsApi>();
            Settings = httpClientGenerator.Generate<IPayMerchantSettingsApi>();
        }
    }
}
