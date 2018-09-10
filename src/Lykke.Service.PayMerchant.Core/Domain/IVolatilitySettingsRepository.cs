using System.Threading.Tasks;

namespace Lykke.Service.PayMerchant.Core.Domain
{
    public interface IVolatilitySettingsRepository
    {
        Task<IVolatilitySettings> AddAsync(IVolatilitySettings src);

        Task<IVolatilitySettings> GetAsync(string merchantId);

        Task<IVolatilitySettings> UpdateAsync(IVolatilitySettings src);

        Task DeleteAsync(string merchantId);
    }
}
