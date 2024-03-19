using AutoMapper;
using Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.DTO
{
    public class Post : ICustomMap
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Writer { get; set; }
        public string Properties { get; set; }

        public int RateCount { get; set; }
        public double RateAverage { get; set; }

        public void CustomMappings(Profile profile)
        {
            profile.CreateMap<Model.Post, DTO.Post>()
                .ForMember(dest => dest.RateCount, opt => opt.MapFrom(src => src.PostRates.Count))
                .ForMember(dest => dest.RateAverage, opt => opt.MapFrom(src => src.PostRates.Average(p => p.Value)));
        }
    }
}
