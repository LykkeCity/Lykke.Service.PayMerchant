using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.PayMerchant.Core.Domain
{
    public interface IMerchantRepository
    {
        Task<IReadOnlyList<IMerchant>> GetAsync();

        Task<IMerchant> GetAsync(string merchantName);

        Task<IReadOnlyList<IMerchant>> FindApiKeyAsync(string apiKey);

        Task<IMerchant> FindEmailAsync(string email);

        Task<IMerchant> InsertAsync(IMerchant merchant);

        Task ReplaceAsync(IMerchant merchant);

        Task DeleteAsync(string merchantName);
    }
}
