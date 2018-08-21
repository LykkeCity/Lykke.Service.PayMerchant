using AutoMapper;
using Lykke.Service.PayMerchant.Core.Domain;

namespace Lykke.Service.PayMerchant.AzureRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IMerchant, MerchantEntity>(MemberList.Source)
                .ForSourceMember(src => src.Id, opt => opt.Ignore());
        }
    }
}
