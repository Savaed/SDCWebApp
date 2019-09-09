﻿using AutoMapper;
using System;

using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Maps
{
    public class SightseeingGroupProfile : Profile
    {
        public SightseeingGroupProfile()
        {
            CreateMap<SightseeingGroup, SightseeingGroupDto>()
               .ForMember(x => x.CreatedAt, options => options.AddTransform(d => d.Truncate(TimeSpan.FromSeconds(1))))
               .ForMember(x => x.UpdatedAt, options => options.AddTransform(d => d != null ? d.Truncate(TimeSpan.FromSeconds(1)) : null))
               .ForMember(x => x.UpdatedAt, options => options.AddTransform(d => d.Equals(DateTime.MinValue) ? null : d));

            CreateMap<SightseeingGroupDto, SightseeingGroup>()
                .ForMember(x => x.ConcurrencyToken, options => options.Ignore())
                .ForMember(x => x.UpdatedAt, options => options.Ignore())
                .ForMember(x => x.CreatedAt, options => options.Ignore());
        }
    }
}
