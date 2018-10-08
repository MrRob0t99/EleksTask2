using AutoMapper;
using EleksTask.Dto;
using EleksTask.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>().ReverseMap();

        CreateMap<GetAllCategoryDto, Category>().ReverseMap();

        CreateMap<ProductDto, Product>().ReverseMap();
    }
}