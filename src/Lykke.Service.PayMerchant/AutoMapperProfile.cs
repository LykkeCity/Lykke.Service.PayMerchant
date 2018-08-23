﻿using AutoMapper;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Name));
        }
    }
}
