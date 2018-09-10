using Autofac;
using Lykke.Service.PayMerchant.Core.Services;
using Lykke.Service.PayMerchant.Services;
using Lykke.Service.PayMerchant.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.PayMerchant.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MerchantService>()
                .As<IMerchantService>();

            builder.RegisterType<MerchantGroupService>()
                .As<IMerchantGroupService>();

            builder.RegisterType<VolatilitySettingsService>()
                .As<IVolatilitySettingsService>();
        }
    }
}
