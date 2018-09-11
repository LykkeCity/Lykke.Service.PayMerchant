using System;
using AutoMapper;
using Lykke.Service.PayMerchant.AzureRepositories;
using Lykke.Service.PayMerchant.Core.Domain;
using Lykke.Service.PayMerchant.Models;

namespace Lykke.Service.PayMerchant
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IMerchant, MerchantModel>(MemberList.Source);

            CreateMap<CreateMerchantRequest, Merchant>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateMerchantRequest, Merchant>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.Ignore());

            CreateMap<AddMerchantGroupModel, MerchantGroup>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Merchants, opt => opt.MapFrom(src => string.Join(Constants.Separator, src.Merchants)));

            CreateMap<IMerchantGroup, MerchantGroupResponse>(MemberList.Destination)
                .ForMember(dest => dest.Merchants, opt => opt.MapFrom(src => src.Merchants.Split(Constants.Separator, StringSplitOptions.None)));

            CreateMap<UpdateMerchantGroupModel, MerchantGroup>(MemberList.Destination)
                .ForMember(dest => dest.Merchants, opt => opt.MapFrom(src => string.Join(Constants.Separator, src.Merchants)))
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
        }
    }
}
