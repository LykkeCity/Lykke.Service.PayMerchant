using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PayMerchant.Core.Domain;

namespace Lykke.Service.PayMerchant.Core.Services
{
    public interface IMerchantService
    {
        Task<IReadOnlyList<IMerchant>> GetAsync();

        Task<IMerchant> GetAsync(string merchantName);

        Task<IMerchant> CreateAsync(IMerchant merchant);

        Task UpdateAsync(IMerchant srcMerchant);

        Task DeleteAsync(string merchantName);
    }
}
