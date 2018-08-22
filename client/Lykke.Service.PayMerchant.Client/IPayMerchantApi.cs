using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.PayMerchant.Client.Models;
using Refit;

namespace Lykke.Service.PayMerchant.Client
{
    /// <summary>
    /// PayMerchant client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPayMerchantApi
    {
        /// <summary>
        /// Get all merchants
        /// </summary>
        /// <returns></returns>
        [Get("/api/merchants")]
        Task<IReadOnlyList<MerchantModel>> GetAllAsync();

        /// <summary>
        /// Get merchant by id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [Get("/api/merchants/{merchantId}")]
        Task<MerchantModel> GetByIdAsync(string merchantId);

        /// <summary>
        /// Creates merchant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/api/merchants")]
        Task<MerchantModel> CreateAsync([Body] CreateMerchantRequest request);

        /// <summary>
        /// Updates merchant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Patch("/api/merchants")]
        Task UpdateAsync([Body] UpdateMerchantRequest request);

        /// <summary>
        /// Updates merchant's public key
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        [Multipart]
        [Post("/api/merchants/{merchantId}/publickey")]
        Task SetPublicKeyAsync(string merchantId, [AliasAs("file")] StreamPart stream);

        /// <summary>
        /// Deletes merchant by id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [Delete("/api/merchants/{merchantId}")]
        Task DeleteAsync(string merchantId);
    }
}
