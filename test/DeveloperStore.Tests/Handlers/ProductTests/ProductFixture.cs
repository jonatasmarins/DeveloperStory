using AutoMapper;
using Bogus;
using DeveloperStore.App.Handlers.Products;
using DeveloperStore.App.Mappers;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Validators;
using FluentValidation;
using NSubstitute;

namespace DeveloperStore.Tests.Handlers.ProductTests
{

    public class ProductFixture
    {
        public IProductRepository _productRepository;
        public IUnitOfWork _unitOfWork;
        public IValidator<Product> _validator;
        public IMapper _mapper;


        public UpdateCommandHandler _Update;
        public AddCommandHandler _Add;
        public DeleteCommandHandler _Delete;

        public GetAllCategoriesQueryHandler _GetAllCategories;
        public GetAllProductsQueryHandler _GetAllProducts;
        public GetByCategoryQueryHandler _GetByCategory;
        public GetByIdQueryHandler _GetById;

        public ProductFixture()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _validator = new ProductValidator();
            _mapper = CreateMapper();

            _Update = new UpdateCommandHandler(_productRepository, _unitOfWork, _validator, _mapper);
            _Add = new AddCommandHandler(_productRepository, _unitOfWork, _validator, _mapper);
            _Delete = new DeleteCommandHandler(_productRepository, _unitOfWork);
            _GetAllCategories = new GetAllCategoriesQueryHandler(_productRepository);
            _GetAllProducts = new GetAllProductsQueryHandler(_productRepository, _mapper);
            _GetByCategory = new GetByCategoryQueryHandler(_productRepository, _mapper);
            _GetById = new GetByIdQueryHandler(_productRepository, _mapper);
        }

        private static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });

            return configuration.CreateMapper();
        }

        public Product? GetProductEmptyOrNull(bool IsNull = false)
        { 
            return IsNull ? null: new Product();
        }

        public IEnumerable<Product>? GetAllProductEmptyOrNull(bool IsNull = false)
        {
            return IsNull ? null : [];
        }

        public IEnumerable<string>? GetCategoriesEmptyOrNull(bool IsNull = false)
        {
            return IsNull ? null : [];
        }

        public IEnumerable<string>? GetCategories(bool IsNull = false)
        {
            return ["Video Game", "CellPhone", "Others"];
        }
    }

    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.Random.Int(0, 1000));
            RuleFor(p => p.Title, f => f.Commerce.ProductName());
            RuleFor(p => p.Price, f => f.Finance.Amount(10, 1000));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]);
            RuleFor(p => p.Image, f => f.Image.PicsumUrl());
            RuleFor(p => p.Rating, f => new Faker<Rating>()
                .RuleFor(r => r.Rate, f => f.Random.Decimal(1, 5))
                .RuleFor(r => r.Count, f => f.Random.Int(0, 1000))
                .RuleFor(r => r.ProductId, f => f.Random.Int(1, 1000))
                .Generate()
            );
        }
    }

    public class UpdateProductCommandRequestFaker : Faker<UpdateProductCommandRequest>
    {
        public UpdateProductCommandRequestFaker()
        {
            RuleFor(p => p.Id, f => f.Random.Int(1, 1000));
            RuleFor(p => p.Title, f => f.Commerce.ProductName());
            RuleFor(p => p.Price, f => f.Finance.Amount(10, 1000));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]);
            RuleFor(p => p.Image, f => f.Image.PicsumUrl());
            RuleFor(p => p.Rating, f => new Faker<GetRatingQueryResponse>()
                .RuleFor(r => r.Rate, f => f.Random.Decimal(1, 5))
                .RuleFor(r => r.Count, f => f.Random.Int(0, 1000))
                .Generate()
            );
        }
    }

    public class AddProductCommandRequestFaker : Faker<AddProductCommandRequest>
    {
        public AddProductCommandRequestFaker()
        {            
            RuleFor(p => p.Title, f => f.Commerce.ProductName());
            RuleFor(p => p.Price, f => f.Finance.Amount(10, 1000));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]);
            RuleFor(p => p.Image, f => f.Image.PicsumUrl());
            RuleFor(p => p.Rating, f => new Faker<GetRatingQueryResponse>()
                .RuleFor(r => r.Rate, f => f.Random.Decimal(1, 5))
                .RuleFor(r => r.Count, f => f.Random.Int(0, 1000))
                .Generate()
            );
        }
    }

    public class DeleteProductCommandRequestFaker : Faker<DeleteProductCommandRequest>
    {
        public DeleteProductCommandRequestFaker()
        {
            RuleFor(x => x.Id, f => f.Random.Int(1, 5));
        }
    }

    public class GetAllQueryRequestFaker : Faker<GetAllQueryRequest>
    {
        public GetAllQueryRequestFaker()
        {
            RuleFor(x => x.Page, f => f.Random.Int(1, 5));
            RuleFor(x => x.Size, f => f.Random.Int(5, 10));
            RuleFor(x => x.Order, "Name");
        }
    }

    public class GetByCategoryQueryRequestFaker : Faker<GetByCategoryQueryRequest>
    {
        public GetByCategoryQueryRequestFaker()
        {
            RuleFor(x => x.Category, f => f.Commerce.Categories(1)[0]);
            RuleFor(x => x.Page, f => f.Random.Int(1, 5));
            RuleFor(x => x.Size, f => f.Random.Int(5, 10));
            RuleFor(x => x.Order, "Name");
        }
    }
}
