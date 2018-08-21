using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.PayMerchant.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public PayMerchantSettings PayMerchantService { get; set; }
    }
}
