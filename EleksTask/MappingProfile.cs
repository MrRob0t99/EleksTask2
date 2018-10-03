using AutoMapper;
using EleksTask.Dto;
using EleksTask.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<CreateProductDto, Product>().ReverseMap();
    }
}