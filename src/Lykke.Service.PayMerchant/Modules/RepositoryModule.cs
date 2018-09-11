using Autofac;
using AzureStorage.Tables;
using AzureStorage.Tables.Templates.Index;
using Lykke.Common.Log;
using Lykke.Service.PayMerchant.AzureRepositories;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.PayMerchant.Modules
{
    public class RepositoryModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public RepositoryModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            const string merchantsTableName = "Merchants";
            const string merchantGroupsTableName = "MerchantGroups";
            const string merchantVolatilitySettingsTableName = "MerchantVolatilitySettings";

            builder.Register(c => new MerchantRepository(
                    AzureTableStorage<MerchantEntity>.Create(
                        _appSettings.ConnectionString(x => x.PayMerchantService.Db.MerchantConnString),
                        merchantsTableName, c.Resolve<ILogFactory>()),
                    AzureTableStorage<AzureIndex>.Create(
                        _appSettings.ConnectionString(x => x.PayMerchantService.Db.MerchantConnString),
                        merchantsTableName, c.Resolve<ILogFactory>())))
                .As<IMerchantRepository>()
                .SingleInstance();

            builder.Register(c => new MerchantGroupRepository(
                    AzureTableStorage<MerchantGroupEntity>.Create(
                        _appSettings.ConnectionString(x => x.PayMerchantService.Db.MerchantConnString),
                        merchantGroupsTableName, c.Resolve<ILogFactory>()),
                    AzureTableStorage<AzureIndex>.Create(
                        _appSettings.ConnectionString(x => x.PayMerchantService.Db.MerchantConnString),
                        merchantGroupsTableName, c.Resolve<ILogFactory>())))
                .As<IMerchantGroupRepository>()
                .SingleInstance();

            builder.Register(c => new VolatilitySettingsRepository(
                    AzureTableStorage<VolatilitySettingsEntity>.Create(
                        _appSettings.ConnectionString(x => x.PayMerchantService.Db.MerchantConnString),
                        merchantVolatilitySettingsTableName, c.Resolve<ILogFactory>())))
                .As<IVolatilitySettingsRepository>()
                .SingleInstance();
        }
    }
}
