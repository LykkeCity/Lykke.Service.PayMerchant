using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
            IReadOnlyList<IMerchant> merchants = await _merchantRepository.FindAsync(merchant.ApiKey);

            if (merchants.Count > 0)
                throw new DuplicateMerchantApiKeyException(merchant.ApiKey);

            IMerchant createdMerchant = await _merchantRepository.InsertAsync(merchant);

            _log.Info("Merchant created", merchant);

            return createdMerchant;
        }

        public async Task UpdateAsync(IMerchant merchant)
        {
            IMerchant existingMerchant = await _merchantRepository.GetAsync(merchant.Name);

            if (existingMerchant == null)
                throw new MerchantNotFoundException(merchant.Name);

            if (merchant.ApiKey != existingMerchant.ApiKey)
            {
                IReadOnlyList<IMerchant> merchants = await _merchantRepository.FindAsync(merchant.ApiKey);

                if (merchants.Count > 0)
                    throw new DuplicateMerchantApiKeyException(merchant.ApiKey);
            }

            Mapper.Map(merchant, existingMerchant);

            await _merchantRepository.ReplaceAsync(existingMerchant);

            _log.Info("Merchant updated", merchant);
        }

        public async Task SetPublicKeyAsync(string merchantName, string publicKey)
        {
            IMerchant merchant = await _merchantRepository.GetAsync(merchantName);

            if (merchant == null)
                throw new MerchantNotFoundException(merchantName);

            merchant.PublicKey = publicKey;

            await _merchantRepository.ReplaceAsync(merchant);

            _log.Info("Merchant public key updated", merchant);
        }

        public async Task DeleteAsync(string merchantName)
        {
            await _merchantRepository.DeleteAsync(merchantName);

            _log.Info("Merchant deleted", new { MerchantId = merchantName });
        }
    }
}
