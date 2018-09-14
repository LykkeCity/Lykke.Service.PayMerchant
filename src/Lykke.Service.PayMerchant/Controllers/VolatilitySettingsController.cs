using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using Lykke.Service.PayMerchant.Core;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Core.Exceptions;
using Lykke.Service.PayMerchant.Core.Services;
using Lykke.Service.PayMerchant.Models;
using LykkePay.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.PayMerchant.Controllers
{
    [Route("api/volatilitySettings")]
    public class VolatilitySettingsController : Controller
    {
        private readonly IVolatilitySettingsService _volatilitySettingsService;
        private readonly IMerchantService _merchantService;
        private readonly ILog _log;

        public VolatilitySettingsController(
            [NotNull] IVolatilitySettingsService volatilitySettingsService,
            [NotNull] ILogFactory logFactory, 
            [NotNull] IMerchantService merchantService)
        {
            _merchantService = merchantService ?? throw new ArgumentNullException(nameof(merchantService));
            _volatilitySettingsService = volatilitySettingsService ?? throw new ArgumentNullException(nameof(volatilitySettingsService));
            _log = logFactory.CreateLog(this);
        }

        /// <summary>
        /// Add new volatility settings for merchant
        /// </summary>
        /// <param name="model">New volatility settings details</param>
        /// <response code="400">Volatility settings already exist</response>
        /// <response code="404">Merchant not found</response>
        /// <response code="201">Volatility settings details</response>
        [HttpPost]
        [SwaggerOperation("AddVolatilitySettings")]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.Created)]
        [ValidateModel]
        public async Task<IActionResult> Add([FromBody] AddVolatilitySettingsModel model)
        {
            IMerchant merchant = await _merchantService.GetAsync(model.MerchantId);

            if (merchant == null)
                return NotFound(ErrorResponse.Create("Merchant not found"));

            try
            {
                await _volatilitySettingsService.AddAsync(new VolatilitySettings
                {
                    MerchantId = model.MerchantId,
                    ZeroCoverageAssetPairs = model.ZeroCoverageAssetPairs
                });

                return Created(Url.Action("Get", new {merchantId = model.MerchantId}), null);
            }
            catch (VolatilitySettingsAlreadyExistException e)
            {
                _log.Error(e, context: model.ToDetails());

                return BadRequest(ErrorResponse.Create(e.Message));
            }
        }

        /// <summary>
        /// Get existing volatility settings for merchant
        /// </summary>
        /// <param name="merchantId">Merchant id</param>
        /// <response code="404">Volatility settings not found</response>
        /// <response code="200">Volatility settings details</response>
        [HttpGet]
        [Route("{merchantId}")]
        [SwaggerOperation("GetVolatilitySettings")]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(VolatilitySettingsResponse), (int) HttpStatusCode.OK)]
        [ValidateModel]
        public async Task<IActionResult> Get([Required, RowKey] string merchantId)
        {
            IVolatilitySettings settings = await _volatilitySettingsService.GetAsync(merchantId);

            if (settings == null)
                return NotFound(ErrorResponse.Create("Merchant volatility settings not found"));

            return Ok(new VolatilitySettingsResponse
            {
                MerchantId = settings.MerchantId,
                ZeroCoverageAssetPairs = settings.ZeroCoverageAssetPairs
            });
        }

        /// <summary>
        /// Update volatility settings
        /// </summary>
        /// <param name="model">Volatility settings details</param>
        /// <response code="200">Updated successfully</response>
        /// <response code="400">Asset pairs empty</response>
        /// <response code="404">Volatility settings not found</response>
        [HttpPatch]
        [SwaggerOperation("UpdateVolatilitySettings")]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ValidateModel]
        public async Task<IActionResult> Update([FromBody] UpdateVolatilitySettingsModel model)
        {
            try
            {
                IVolatilitySettings updatedSettings = await _volatilitySettingsService.UpdateAsync(
                    new VolatilitySettings
                    {
                        MerchantId = model.MerchantId,
                        ZeroCoverageAssetPairs = model.ZeroCoverageAssetPairs
                    });

                if (updatedSettings == null)
                    return NotFound(ErrorResponse.Create("Merchant volatility settings not found"));

                return Ok();
            }
            catch (VolatilitySettingsEmptyException e)
            {
                _log.Error(e, "Asset pairs value is empty");

                return BadRequest(ErrorResponse.Create(e.Message));
            }
        }

        /// <summary>
        /// Delete volatility settings for merchant
        /// </summary>
        /// <param name="merchantId">Merchant id</param>
        /// <response code="200">Volatility settings successfully deleted</response>
        /// <response code="404">Volatility settings for merchant not found</response>
        [HttpDelete]
        [Route("{merchantId}")]
        [SwaggerOperation("DeleteVolatilitySettings")]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ValidateModel]
        public async Task<IActionResult> Delete([Required, RowKey] string merchantId)
        {
            try
            {
                await _volatilitySettingsService.RemoveAsync(merchantId);

                return Ok();
            }
            catch (VolatilitySettingsNotFoundException e)
            {
                _log.Error(e, $"Volatility settings for merchant {merchantId} not found");

                return NotFound(ErrorResponse.Create(e.Message));
            }
        }
    }
}
