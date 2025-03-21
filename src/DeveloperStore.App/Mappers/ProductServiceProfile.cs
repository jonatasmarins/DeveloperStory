using AutoMapper;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Services.Models;

namespace DeveloperStore.App.Mappers
{
    public class ProductServiceProfile : Profile
    {
        public ProductServiceProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(ResultResponse<>))
                .ReverseMap();

            CreateMap<Product, GetProductsQueryResponse>()
                .ReverseMap();

            CreateMap<Rating, GetRatingQueryResponse>()
                .ReverseMap();

            CreateMap<Product, GetByIdQueryResponse>()
                .ReverseMap();

            CreateMap<AddCommandRequest, Product>()
                .ReverseMap();

            CreateMap<AddCommandResponse, Product>()
                .ReverseMap();

            CreateMap<UpdateCommandRequest, Product>()
                .ConvertUsing<ProductResolver>();

            CreateMap<UpdateCommandResponse, Product>()
                .ReverseMap();

            CreateMap<GetByCategoryQueryResponse, Product>()
                .ReverseMap();
        }
    }

    public class ProductResolver : ITypeConverter<UpdateCommandRequest, Product>
    {
        public Product Convert(UpdateCommandRequest source, Product destination, ResolutionContext context)
        {
            destination.Title = source.Title;
            destination.Price = source.Price;
            destination.Description = source.Description;
            destination.Category = source.Category;
            destination.Image = source.Image;
            destination.Rating.Count = source.Rating.Count;
            destination.Rating.Rate = source.Rating.Rate;
            destination.UpdatedAt = DateTime.Now;
            destination.CreatedAt = destination.CreatedAt;

            return destination;
        }
    }
}
