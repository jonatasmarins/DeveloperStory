using AutoMapper;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.App.Models;

namespace DeveloperStore.App.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(ResultResponse<>))
                .ReverseMap();

            CreateMap<Product, GetProductsQueryResponse>()
                .ReverseMap();

            CreateMap<Rating, GetRatingQueryResponse>()
                .ReverseMap();

            CreateMap<Product, GetByIdQueryResponse>()
                .ReverseMap();

            CreateMap<AddProductCommandRequest, Product>()
                .ReverseMap();

            CreateMap<AddProductCommandResponse, Product>()
                .ReverseMap();

            CreateMap<UpdateProductCommandRequest, Product>()
                .ConvertUsing<ProductResolver>();

            CreateMap<UpdateProductCommandResponse, Product>()
                .ReverseMap();

            CreateMap<GetByCategoryQueryResponse, Product>()
                .ReverseMap();
        }
    }

    public class ProductResolver : ITypeConverter<UpdateProductCommandRequest, Product>
    {
        public Product Convert(UpdateProductCommandRequest source, Product destination, ResolutionContext context)
        {
            destination ??= new Product();

            destination.Title = source.Title;
            destination.Price = source.Price;
            destination.Description = source.Description;
            destination.Category = source.Category;
            destination.Image = source.Image;
            destination.Rating.ProductId = source.Id;
            destination.Rating.Count = source.Rating.Count;
            destination.Rating.Rate = source.Rating.Rate;
            destination.UpdatedAt = DateTime.Now;
            destination.CreatedAt = destination.CreatedAt;

            return destination;
        }
    }
}
