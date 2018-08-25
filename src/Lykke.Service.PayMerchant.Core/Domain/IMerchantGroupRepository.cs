using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.PayMerchant.Core.Domain
{
    public interface IMerchantGroupRepository
    {
        Task<IMerchantGroup> CreateAsync(IMerchantGroup src);

        Task<IMerchantGroup> GetAsync(string id);

        Task UpdateAsync(IMerchantGroup src);

        Task DeleteAsync(string id);

        Task<IReadOnlyList<IMerchantGroup>> GetByOwnerAsync(string ownerId);
    }
}
