using AutoMapper;
using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Queries.Cart.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.App.Models;
using static DeveloperStore.App.Models.Commands.Cart.Request.AddCartCommandRequest;
using static DeveloperStore.App.Models.Commands.Cart.Response.UpdateCartCommandResponse;
using static DeveloperStore.App.Models.Queries.Cart.Response.GetAllCartQueryResponse;
using static DeveloperStore.App.Models.Queries.Cart.Response.GetByIdCartQueryResponse;

namespace DeveloperStore.App.Mappers
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {

            CreateMap(typeof(PaginatedResult<>), typeof(ResultResponse<>))
                    .ReverseMap();

            #region Add

            CreateMap<AddCartCommandRequest, Cart>()
                .ConvertUsing<AddCartRequestToEntityResolver>();

            #endregion

            #region GetAll

            CreateMap<GetAllCartQueryResponse, Cart>()
                    .ReverseMap();

            CreateMap<GetCartProductQueryResponse, CartProduct>()
                    .ReverseMap();

            #endregion

            #region GetById

            CreateMap<GetByIdCartQueryResponse, Cart>()
                    .ReverseMap();

            CreateMap<GetBydIdCartProductQueryResponse, CartProduct>()
                    .ReverseMap();

            #endregion

            #region Update

            CreateMap<UpdateCartCommandRequest, Cart>()
                .ConvertUsing<UpdateCartRequestToEntityResolver>();

            CreateMap<UpdateCartCommandResponse, Cart>()
                .ReverseMap();

            CreateMap<UpdateCartProductCommandResponse, CartProduct>()
                .ReverseMap();

            #endregion
        }


        public static void MapCartProperties<T>(IEnumerable<T> products, Cart destination, Func<T, int> getProductId, Func<T, int> getQuantity)
        {
            foreach (var item in products)
            {
                destination.Products.Add(new CartProduct()
                {
                    CartId = destination.Id,
                    ProductId = getProductId(item),
                    Quantity = getQuantity(item),
                });
            }
        }
    }

    public class AddCartRequestToEntityResolver : ITypeConverter<AddCartCommandRequest, Cart>
    {
        public Cart Convert(AddCartCommandRequest source, Cart destination, ResolutionContext context)
        {
            destination ??= new Cart();

            destination.UserId = source.UserId;
            destination.Date = source.Date;

            CartProfile.MapCartProperties(source.Products, destination, item => item.ProductId, item => item.Quantity);

            return destination;
        }
    }

    public class UpdateCartRequestToEntityResolver : ITypeConverter<UpdateCartCommandRequest, Cart>
    {
        public Cart Convert(UpdateCartCommandRequest source, Cart destination, ResolutionContext context)
        {
            destination ??= new Cart();

            destination.UserId = source.UserId;
            destination.Date = source.Date;

            destination.Products.Clear();

            CartProfile.MapCartProperties(source.Products, destination, item => item.ProductId, item => item.Quantity);

            return destination;
        }
    }
}
