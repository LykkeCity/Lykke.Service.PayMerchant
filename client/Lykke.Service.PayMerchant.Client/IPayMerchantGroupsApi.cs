using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.PayMerchant.Client.Models;
using Refit;

namespace Lykke.Service.PayMerchant.Client
{
    /// <summary>
    /// PayMerchant groups client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPayMerchantGroupsApi
    {
        /// <summary>
        /// Create new group
        /// </summary>
        /// <param name="request">New merchant group details</param>
        /// <returns></returns>
        [Post("/api/merchantGroups")]
        Task<MerchantGroupResponse> AddGroupAsync([Body] AddMerchantGroupRequest request);

        /// <summary>
        /// Get merchant group by id
        /// </summary>
        /// <param name="id">Merchant group id</param>
        /// <returns></returns>
        [Get("/api/merchantGroups/{id}")]
        Task<MerchantGroupResponse> GetGroupAsync(string id);

        /// <summary>
        /// Update merchant group details
        /// </summary>
        /// <param name="request">Merchant group updaterequest details</param>
        /// <returns></returns>
        [Put("/api/merchantGroups")]
        Task UpdateGroupAsync([Body] UpdateMerchantGroupRequest request);

        /// <summary>
        /// Gelete merchant group by id
        /// </summary>
        /// <param name="id">Merchant group id</param>
        /// <returns></returns>
        [Delete("/api/merchantGroups/{id}")]
        Task DeleteGroupAsync(string id);

        /// <summary>
        /// Get merchants from all groups by type and owner
        /// </summary>
        /// <param name="request">Request details</param>
        /// <returns></returns>
        [Post("/api/merchantGroups/merchants/byUsage")]
        Task<MerchantsByUsageResponse> GetMerchantsByUsageAsync([Body] GetMerchantsByUsageRequest request);

        /// <summary>
        /// Get all merchant groups by owner
        /// </summary>
        /// <param name="ownerId">Owner id</param>
        /// <returns></returns>
        [Get("/api/merchantGroups/byOwner/{ownerId}")]
        Task<IEnumerable<MerchantGroupResponse>> GetGroupsByOwnerAsync(string ownerId);
    }
}
