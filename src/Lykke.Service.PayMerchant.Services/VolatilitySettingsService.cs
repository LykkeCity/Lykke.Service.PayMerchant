using System;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Lykke.Service.PayMerchant.Core;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;
using Lykke.Service.PayMerchant.Core.Services;

namespace Lykke.Service.PayMerchant.Services
{
    public class VolatilitySettingsService : IVolatilitySettingsService
    {
        private readonly IVolatilitySettingsRepository _repository;
        private readonly ILog _log;

        public VolatilitySettingsService(
            [NotNull] IVolatilitySettingsRepository repository,
            [NotNull] ILogFactory logFactory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _log = logFactory.CreateLog(this);
        }

        public async Task<IVolatilitySettings> AddAsync(IVolatilitySettings src)
        {
            try
            {
                IVolatilitySettings newSettings = await _repository.AddAsync(src);

                _log.Info("Merchant volatility settings added", newSettings.ToDetails());

                return newSettings;
            }
            catch (DuplicateKeyException e)
            {
                _log.Error(e, context: src.ToDetails());

                throw new VolatilitySettingsAlreadyExistException(src.MerchantId);
            }
        }

        public async Task<IVolatilitySettings> GetAsync(string merchantId)
        {
            try
            {
                return await _repository.GetAsync(merchantId);
            }
            catch (KeyNotFoundException e)
            {
                _log.Warning("Merchant volatility coverage settings not found", e);

                return null;
            }
        }

        public async Task<IVolatilitySettings> UpdateAsync(IVolatilitySettings src)
        {
            try
            {
                if (string.IsNullOrEmpty(src.ZeroCoverageAssetPairs))
                    throw new VolatilitySettingsEmptyException(src.MerchantId);

                IVolatilitySettings updatedSettings = await _repository.UpdateAsync(src);

                _log.Info("Merchant volatility settings have been updated", updatedSettings.ToDetails());

                return updatedSettings;
            }
            catch (KeyNotFoundException e)
            {
                _log.Warning("Merchant volatility settings not found", e);

                return null;
            }
        }

        public async Task RemoveAsync(string merchantId)
        {
            try
            {
                await _repository.DeleteAsync(merchantId);
            }
            catch (KeyNotFoundException e)
            {
                _log.Error(e, $"Merchant id = {merchantId}");

                throw new VolatilitySettingsNotFoundException(merchantId);
            }
        }
    }
}
