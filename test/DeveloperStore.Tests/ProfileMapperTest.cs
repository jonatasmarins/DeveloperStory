using AutoMapper;
using Bogus;
using DeveloperStore.App.Mappers;
using DeveloperStore.App.Models.Queries;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.App.Models;

namespace DeveloperStore.Tests
{
    //public class ProfileMapperTest
    //{
    //    IMapper _mapper;

    //    public ProfileMapperTest()
    //    {
    //        CreateMapper();
    //    }

    //    [Fact]
    //    public void MapperProducts()
    //    {
    //        //Arrange
    //        var currentPage = 1;
    //        var pageSize = 10;

    //        var queryRequest = new PaginatedQueryRequest(string.Empty, currentPage, pageSize);

    //        var productsFaker = new Faker<Product>();

    //        IEnumerable<Product> products = productsFaker.Generate(5);

    //        var pagination = new PaginatedResult<IEnumerable<Product>>
    //        {
    //            CurrentPage = queryRequest.Page,
    //            PageSize = queryRequest.Size,
    //            TotalItems = products.Count(),
    //            Data = products
    //        };


    //        //Action
    //        var result = _mapper.Map<ResultResponse<IEnumerable<Product>>>(pagination);    


    //        //Assert
    //        Assert.Equal(queryRequest.Page, result.CurrentPage);
    //        Assert.Equal(queryRequest.Size, result.PageSize);
    //        Assert.Equal(pagination.TotalItems, result.TotalItems);
    //        Assert.NotEqual(0, pagination.TotalPages);
    //    }

    //    [Fact]
    //    public void MapperProdutToResponse()
    //    {
    //        //Arrange
    //        var currentPage = 1;
    //        var pageSize = 10;

    //        var queryRequest = new PaginatedQueryRequest(string.Empty, currentPage, pageSize);

    //        var productsFaker = new Faker<Product>();

    //        IEnumerable<Product> products = productsFaker.Generate(5);

    //        var pagination = new ResultResponse<IEnumerable<Product>>
    //        {
    //            CurrentPage = queryRequest.Page,
    //            PageSize = queryRequest.Size,
    //            TotalItems = products.Count(),
    //            Data = products
    //        };

    //        var teste2 = _mapper.Map<IEnumerable<GetProductsQueryResponse>>(pagination.Data);


    //        //Action
    //        var result = _mapper.Map<ResultResponse<IEnumerable<GetProductsQueryResponse>>>(pagination);


    //        //Assert
    //        Assert.Equal(queryRequest.Page, result.CurrentPage);
    //        Assert.Equal(queryRequest.Size, result.PageSize);
    //        Assert.Equal(pagination.TotalItems, result.TotalItems);
    //        Assert.NotEqual(0, pagination.TotalPages);
    //    }

    //    private void CreateMapper()
    //    {
    //        var configuration = new MapperConfiguration(cfg =>
    //        {
    //            cfg.AddProfile(new ProductServiceProfile());
    //        });


    //        _mapper = configuration.CreateMapper();
    //    }
    //}
}
