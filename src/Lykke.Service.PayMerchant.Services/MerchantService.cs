using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;
using Lykke.Service.PayMerchant.Core.Services;

namespace Lykke.Service.PayMerchant.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILog _log;

        public MerchantService(
            [NotNull] IMerchantRepository merchantRepository,
            [NotNull] ILogFactory logFactory)
        {
            _merchantRepository = merchantRepository;
            _log = logFactory.CreateLog(this);
        }

        public async Task<IReadOnlyList<IMerchant>> GetAsync()
        {
            return await _merchantRepository.GetAsync();
        }

        public async Task<IMerchant> GetAsync(string merchantName)
        {
            return await _merchantRepository.GetAsync(merchantName);
        }

        public async Task<IMerchant> CreateAsync(IMerchant merchant)
        {
            IReadOnlyList<IMerchant> apiKeyMerchants = await _merchantRepository.FindApiKeyAsync(merchant.ApiKey);

            if (apiKeyMerchants.Any())
                throw new DuplicateMerchantApiKeyException(merchant.ApiKey);

            IMerchant emailMerchant = await _merchantRepository.FindEmailAsync(merchant.Email);

            if (emailMerchant != null)
                throw new DuplicateMerchantEmailException(merchant.Email);

            IMerchant createdMerchant = await _merchantRepository.InsertAsync(merchant);

            _log.Info("Merchant created", merchant);

            return createdMerchant;
        }

        public async Task UpdateAsync(IMerchant srcMerchant)
        {
            IMerchant existingMerchant = await _merchantRepository.GetAsync(srcMerchant.Name);

            if (existingMerchant == null)
                throw new MerchantNotFoundException(srcMerchant.Name);

            if (srcMerchant.ApiKey != existingMerchant.ApiKey)
            {
                IReadOnlyList<IMerchant> merchants = await _merchantRepository.FindApiKeyAsync(srcMerchant.ApiKey);

                if (merchants.Any())
                    throw new DuplicateMerchantApiKeyException(srcMerchant.ApiKey);
            }

            if (string.IsNullOrEmpty(srcMerchant.Email))
            {
                srcMerchant.Email = existingMerchant.Email;
            }
            else
            {
                if (!string.IsNullOrEmpty(existingMerchant.Email))
                    throw new MerchantEmailUpdateException(existingMerchant.Id);

                IMerchant emailMerchant = await _merchantRepository.FindEmailAsync(srcMerchant.Email);

                if (emailMerchant != null)
                    throw new DuplicateMerchantEmailException(srcMerchant.Email);
            }

            await _merchantRepository.ReplaceAsync(srcMerchant);

            _log.Info("Merchant updated", srcMerchant);
        }

        public async Task DeleteAsync(string merchantName)
        {
            await _merchantRepository.DeleteAsync(merchantName);

            _log.Info("Merchant deleted", new { MerchantId = merchantName });
        }
    }
}
