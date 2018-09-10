using System.Threading.Tasks;
using Lykke.Service.PayMerchant.Core.Domain;

namespace Lykke.Service.PayMerchant.Core.Services
{
    public interface IVolatilitySettingsService
    {
        Task<IVolatilitySettings> AddAsync(IVolatilitySettings src);

        Task<IVolatilitySettings> GetAsync(string merchantId);

        Task<IVolatilitySettings> UpdateAsync(IVolatilitySettings src);

        Task RemoveAsync(string merchantId);
    }
}
