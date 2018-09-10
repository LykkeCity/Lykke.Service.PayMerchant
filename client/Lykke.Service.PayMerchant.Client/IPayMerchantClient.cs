using JetBrains.Annotations;

namespace Lykke.Service.PayMerchant.Client
{
    /// <summary>
    /// PayMerchant client interface.
    /// </summary>
    [PublicAPI]
    public interface IPayMerchantClient
    {
        /// <summary>
        /// Merchants API
        /// </summary>
        IPayMerchantApi Api { get; }

        /// <summary>
        /// Merchant groups API
        /// </summary>
        IPayMerchantGroupsApi Groups { get; }

        /// <summary>
        /// Merchant settings API
        /// </summary>
        IPayMerchantSettingsApi Settings { get; set; }
    }
}
