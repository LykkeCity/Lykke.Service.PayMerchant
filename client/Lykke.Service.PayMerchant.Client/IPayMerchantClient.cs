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
        /// Merchants Api interface
        /// </summary>
        IPayMerchantApi Api { get; }

        /// <summary>
        /// Merchant groups Api interface
        /// </summary>
        IPayMerchantGroupsApi GroupsApi { get; }
    }
}
