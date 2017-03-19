using AutoMapper;
using MaiTrinhWeb.Data;
using MaiTrinhWeb.Models;

namespace MaiTrinhWeb
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(
                config =>
                {
                    config.CreateMap<Product, ProductViewModel>().ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color.Name));
                });
        }
    }
}