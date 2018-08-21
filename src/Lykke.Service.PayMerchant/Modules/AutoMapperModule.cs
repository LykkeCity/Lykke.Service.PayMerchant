using Autofac;
using AutoMapper;

namespace Lykke.Service.PayMerchant.Modules
{
    public class AutoMapperModule : Module
    {
        private readonly IComponentContext _componentContext;

        public AutoMapperModule(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(_componentContext.Resolve);
                cfg.AddProfiles(typeof(AutoMapperProfile));
                cfg.AddProfiles(typeof(AzureRepositories.AutoMapperProfile));
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
