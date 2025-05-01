using ProductCatalog.Models;
using ProductCatalog.Products.Features.CreateProduct;
using ProductCatalog.Products.Features.GetProducts;
using ProductCatalog.Products.Features.UpdateProduct;

namespace ProductCatalog.Products.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<CreateProductCommand, Product>()
                .ForMember(destination => destination.Id, opt => opt.Ignore());
            CreateMap<GetProductsRequest, GetProductsQuery>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>()
                .ForCtorParam("Id", opt => opt.MapFrom((src, ctx) => ctx.Items["Id"]));
        }
    }
}
