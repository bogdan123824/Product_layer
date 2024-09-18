using AutoMapper;
using Shop.BusinessLayer.DTO;
using Shop.DataAccessLayer.Entities;
using Shop.Models;

namespace Shop.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerDTO>().ReverseMap();
            CreateMap<AddProductRequest, ProductDTO>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => new CategoryDTO { Name = x.Category }))
                .ForMember(x => x.Manufacturer, opt => opt.MapFrom(x => new ManufacturerDTO { Name = x.Manufacturer }));
            CreateMap<UpdateProductRequest, ProductDTO>();
            CreateMap<ProductDTO, ProductResponse>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Manufacturer, opt => opt.MapFrom(x => x.Manufacturer.Name));
        }
    }
}
