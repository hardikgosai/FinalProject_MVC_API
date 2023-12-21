using AutoMapper;
using Domain.Models;
using Getri_FinalProject_MVC_API.DTO;

namespace Getri_FinalProject_MVC_API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
