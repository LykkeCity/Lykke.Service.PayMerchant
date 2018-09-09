using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.PayMerchant.Client.Models;
using Refit;

namespace Lykke.Service.PayMerchant.Client
{
    /// <summary>
    /// PayMerchant settings API
    /// </summary>
    [PublicAPI]
    public interface IPayMerchantSettingsApi
    {
        /// <summary>
        /// Returns merchant volatility settings
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [Get("/api/volatilitySettings/{merchantId}")]
        Task<VolatilitySettingsResponse> GetVolatilitySettingsAsync(string merchantId);

        /// <summary>
        /// Adds new volatility settings for merchant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/api/volatilitySettings")]
        Task AddVolatilitySettingsAsync([Body] AddVolatilitySettingsRequest request);

        /// <summary>
        /// Updates volatility settings for merchant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Patch("/api/volatilitySettings")]
        Task UpdateVolatilitySettingsAsync([Body] UpdateVolatilitySettingsRequest request);

        /// <summary>
        /// Deletes volatility settings for merchant
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [Delete("/api/volatilitySettings/{merchantId}")]
        Task DeleteVolatilitySettingsAsync(string merchantId);
    }
}
