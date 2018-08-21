using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using Lykke.Service.PayMerchant.Core;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;
using Lykke.Service.PayMerchant.Core.Services;
using Lykke.Service.PayMerchant.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.PayMerchant.Controllers
{
    [ApiController]
    [Route("api/merchants")]
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        private readonly ILog _log;

        public MerchantsController(
            [NotNull] IMerchantService merchantService,
            [NotNull] ILogFactory logFactory)
        {
            _merchantService = merchantService ?? throw new ArgumentNullException(nameof(merchantService));
            _log = logFactory.CreateLog(this);
        }

        /// <summary>
        /// Returns all merchants.
        /// </summary>
        /// <returns>The collection of merchants.</returns>
        /// <response code="200">The collection of merchants.</response>
        [HttpGet]
        [SwaggerOperation("MerchantsGetAll")]
        [ProducesResponseType(typeof(List<MerchantModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            IReadOnlyList<IMerchant> merchants = await _merchantService.GetAsync();

            var model = Mapper.Map<List<MerchantModel>>(merchants);

            return Ok(model);
        }

        /// <summary>
        /// Returns merchant.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <returns>The merchant.</returns>
        /// <response code="200">The merchant.</response>
        /// <response code="400">Invalid merchant id value</response>
        /// <response code="404">The merchant not found.</response>
        [HttpGet]
        [Route("{merchantId}")]
        [SwaggerOperation("MerchantsGetById")]
        [ProducesResponseType(typeof(MerchantModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync(string merchantId)
        {
            try
            {
                IMerchant merchant = await _merchantService.GetAsync(Uri.UnescapeDataString(merchantId));

                if (merchant == null)
                    return NotFound(ErrorResponse.Create("Merchant not found"));

                return Ok(Mapper.Map<MerchantModel>(merchant));
            }
            catch (InvalidRowKeyValueException e)
            {
                _log.Error(e, $"{e.Variable}: {e.Value}");

                return BadRequest(ErrorResponse.Create(e.Message));
            }
        }

        /// <summary>
        /// Creates merchant.
        /// </summary>
        /// <returns>The created merchant.</returns>
        /// <param name="request">The merchant create request.</param>
        /// <response code="200">The created merchant.</response>
        /// <response code="400">Invalid model, duplicate merchant name or api key</response>
        [HttpPost]
        [SwaggerOperation("MerchantsCreate")]
        [ProducesResponseType(typeof(MerchantModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMerchantRequest request)
        {
            try
            {
                var merchant = Mapper.Map<Merchant>(request);

                IMerchant createdMerchant = await _merchantService.CreateAsync(merchant);

                return Ok(Mapper.Map<MerchantModel>(createdMerchant));
            }
            catch (InvalidRowKeyValueException e)
            {
                _log.Error(e, $"{e.Variable}: {e.Value}");

                return BadRequest(ErrorResponse.Create(e.Message));
            }
            catch (Exception exception) when (exception is DuplicateMerchantNameException ||
                                              exception is DuplicateMerchantApiKeyException)
            {
                _log.Warning(exception.Message, context: request.ToDetails());

                return BadRequest(ErrorResponse.Create(exception.Message));
            }
        }
    }
}
