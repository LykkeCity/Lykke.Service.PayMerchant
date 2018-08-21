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

        Task UpdateAsync(IMerchant merchant);

        Task SetPublicKeyAsync(string merchantName, string publicKey);

        Task DeleteAsync(string merchantName);
    }
}
