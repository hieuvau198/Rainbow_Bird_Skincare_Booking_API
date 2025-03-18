using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class HashtagMappingProfile : Profile
    {
        public HashtagMappingProfile() 
        {
            CreateMap<Hashtag, HashtagDto>();
            CreateMap<CreateHashtagDto, Hashtag>();
            CreateMap<UpdateHashtagDto, Hashtag>();
        }
    }
}
