using JetBrains.Annotations;

namespace Lykke.Service.PayMerchant.Client
{
    /// <summary>
    /// PayMerchant client interface.
    /// </summary>
    [PublicAPI]
    public interface IPayMerchantClient
    {
        /// <summary>Application Api interface</summary>
        IPayMerchantApi Api { get; }
    }
}
