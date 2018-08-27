using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Lykke.Service.PayMerchant.Core;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;
using Lykke.Service.PayMerchant.Core.Services;
using KeyNotFoundException = Lykke.Service.PayMerchant.Core.Exceptions.KeyNotFoundException;

namespace Lykke.Service.PayMerchant.Services
{
    public class MerchantGroupService : IMerchantGroupService
    {
        private readonly IMerchantGroupRepository _merchantGroupRepository;
        private readonly ILog _log;

        public MerchantGroupService(
            [NotNull] IMerchantGroupRepository merchantGroupRepository,
            [NotNull] ILogFactory logFactory)
        {
            _merchantGroupRepository = merchantGroupRepository ??
                                       throw new ArgumentNullException(nameof(merchantGroupRepository));
            _log = logFactory.CreateLog(this);
        }

        public Task<IMerchantGroup> CreateAsync(IMerchantGroup src)
        {
            try
            {
                return _merchantGroupRepository.CreateAsync(src);
            }
            catch (DuplicateKeyException ex)
            {
                _log.Error(ex, context: src.ToDetails());

                throw new MerchantGroupAlreadyExistsException(src.DisplayName);
            }
        }

        public Task<IMerchantGroup> GetAsync(string id)
        {
            return _merchantGroupRepository.GetAsync(id);
        }

        public async Task UpdateAsync(IMerchantGroup src)
        {
            try
            {
                await _merchantGroupRepository.UpdateAsync(src);
            }
            catch (KeyNotFoundException ex)
            {
                _log.Error(ex, context: src.ToDetails());

                throw new MerchantGroupNotFoundException(src.Id);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                await _merchantGroupRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                _log.Error(ex, context: $"MerchantGroupId = {id}");

                throw new MerchantGroupNotFoundException(id);
            }
        }

        public async Task<IReadOnlyList<string>> GetMerchantsByUsageAsync(string merchantId,
            MerchantGroupUse merchantGroupUse)
        {
            IReadOnlyList<IMerchantGroup> groups = await _merchantGroupRepository.GetByOwnerAsync(merchantId);

            return groups
                .Where(x => x.MerchantGroupUse == merchantGroupUse)
                .SelectMany(x => x.Merchants?.Split(AzureRepositories.Constants.Separator))
                .Distinct()
                .ToList();
        }

        public Task<IReadOnlyList<IMerchantGroup>> GetByOwnerAsync(string ownerId)
        {
            return _merchantGroupRepository.GetByOwnerAsync(ownerId);
        }
    }
}
