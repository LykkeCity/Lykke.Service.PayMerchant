using Autofac;
using AutoMapper;

namespace Lykke.Service.PayMerchant.Modules
{
    public class AutoMapperModule : Module
    {
        private readonly IContainer _container;

        public AutoMapperModule(IContainer container)
        {
            _container = container;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(_container.Resolve);
                cfg.AddProfiles(typeof(AutoMapperProfile));
                cfg.AddProfiles(typeof(AzureRepositories.AutoMapperProfile));
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
