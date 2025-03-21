using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using DeveloperStore.Infra.Repositories;
using DeveloperStore.Tests.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.Tests.Repositories
{
    public class ProductRepositoryTest
    {
        private ProductRepositoryTestFixture _fixture;

        public ProductRepositoryTest()
        {
            _fixture = new ProductRepositoryTestFixture();
        }

        [Fact(DisplayName = "Get All Products With Success")]
        [Trait("Get All", "Success")]
        public async Task ItShould_GetAll_Success()
        {
            // Arrange
            var options = new QueryOptions();

            int qtd = 25;
            
            int totalPages = (int)Math.Ceiling(qtd / (double)options.Size);

            var products = ProductRepositoryFaker.CreateProductFaker(qtd);

            await _fixture.AddProducts(products);

            // Act
            var result = await _fixture.GetAllAsync(options);

            // Assert
            Assert.Equal(qtd, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.NotNull(result.Data);
        }

        [Fact(DisplayName = "Get All Products Not Found")]
        [Trait("Get All", "Not Found")]
        public async Task ItShould_GetAllNotFound_Success()
        {
            // Arrange
            var options = new QueryOptions();

            int qtd = 0;

            int totalPages = (int)Math.Ceiling(qtd / (double)options.Size);            

            // Act
            var result = await _fixture.GetAllAsync(options);

            // Assert
            Assert.Equal(qtd, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.False(result.Data.Any());
        }

        [Theory(DisplayName = "Get All Products QueryOptions IsAsNoTracking and IgnoreAutoIncludes")]
        [Trait("Get All", "QueryOptions IsNoTracking and IgnoreAutoIncludes")]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task ItShould_GetAllQueryOptions_Success(bool IsAsNoTracking, bool IsIgnoreAutoIncludes)
        {
            // Arrange
            var options = new QueryOptions
            {
                IsAsNoTracking = IsAsNoTracking,
                IsIgnoreAutoIncludes = IsIgnoreAutoIncludes
            };

            var products = ProductRepositoryFaker.CreateProductFaker(10);

            await _fixture.AddProducts(products);

            using var scope = _fixture.Provider.CreateScope();

            var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
            var repository = new ProductRepository(dbcontext);            

            // Act
            var result = await repository.GetAllAsync(options);

            // Assert
            var entry = result.Data.First();
            entry.Category = "OutraCategoria";
            if(IsAsNoTracking)
                Assert.Equal(EntityState.Detached, dbcontext.Entry(entry).State);
            else
                Assert.Equal(EntityState.Modified, dbcontext.Entry(entry).State);


            var loadedCategories = result.Data.Where(c => c.Rating.ProductId == c.Id).ToList();
            var isIgnoreAutoIncludes = loadedCategories.Count == 0;
            if (IsIgnoreAutoIncludes) Assert.True(isIgnoreAutoIncludes);
            else Assert.False(isIgnoreAutoIncludes);
        }

        [Fact(DisplayName = "GetById With Success")]
        [Trait("GetById", "Success")]
        public async Task ItShould_GetById_Success()
        {
            // Arrange
            var options = new QueryOptions();

            int qtd = 25;

            int totalPages = (int)Math.Ceiling(qtd / (double)options.Size);

            var products = ProductRepositoryFaker.CreateProductFaker(qtd);

            await _fixture.AddProducts(products);

            var productId = ProductRepositoryFaker.GetRandomProductId(qtd);

            // Act
            var result = await _fixture.GetByIdAsync(productId, options);

            // Assert
            Assert.Equal(result.Id, productId);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Add With Success")]
        [Trait("Add", "Success")]
        public async Task ItShould_Add_Success()
        {
            // Arrange
            var options = new QueryOptions
            { 
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            };

            int qtd = 1;            

            var product = ProductRepositoryFaker.CreateProductFaker(qtd).First();

            // Act
            await _fixture.AddAsync(product);

            var result = await _fixture.GetByIdAsync(product.Id, options);

            // Assert            
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "AddRange With Success")]
        [Trait("Add", "Success")]
        public async Task ItShould_AddRange_Success()
        {
            // Arrange
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            };

            int qtd = 5;

            var products = ProductRepositoryFaker.CreateProductFaker(qtd);

            // Act
            await _fixture.AddRangeAsync(products);

            var result = await _fixture.GetAllAsync(options);

            // Assert            
            Assert.Equal(qtd, result.TotalItems);
        }

        [Fact(DisplayName = "Update With Success")]
        [Trait("Update", "Success")]
        public async Task ItShould_Update_Success()
        {
            // Arrange
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            };

            int qtd = 1;

            var products = ProductRepositoryFaker.CreateProductFaker(qtd);

            string updateTitle = "Update Title";

            await _fixture.AddProducts(products);

            var updateProduct = products.First();

            updateProduct.Title = updateTitle;

            // Act
            await _fixture.UpdateAsync(updateProduct);

            // Assert
            
            var productUpdate = await _fixture.GetAllAsync(options);

            Assert.Equal(updateTitle, productUpdate.Data.First().Title);
        }

        [Fact(DisplayName = "Delete With Success")]
        [Trait("Delete", "Success")]
        public async Task ItShould_Delete_Success()
        {
            // Arrange
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            };

            int qtd = 5;

            var productsfake = ProductRepositoryFaker.CreateProductFaker(qtd);

            await _fixture.AddRangeAsync(productsfake);

            // Act
            await _fixture.DeleteAsync(1);

            // Assert            
            var products = await _fixture.GetAllAsync(options);

            bool isDelete = products.Data.Where(x => x.Id == 1).Any();

            Assert.False(isDelete);
            Assert.Equal(4, products.TotalItems);
        }

        [Fact(DisplayName = "DeleteRange With Success")]
        [Trait("DeleteRange", "Success")]
        public async Task ItShould_DeleteRange_Success()
        {
            // Arrange
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            };

            int qtd = 5;

            var productsfake = ProductRepositoryFaker.CreateProductFaker(qtd);

            await _fixture.AddRangeAsync(productsfake);

            var productsToDelete = await _fixture.GetAllAsync(options);

            // Act
            await _fixture.DeleteRangeAsync(productsToDelete.Data);

            // Assert            
            var products = await _fixture.GetAllAsync(options);

            Assert.False(products.Data.Any());
            Assert.Equal(0, products.TotalItems);
        }

        //TODO - Fazer as validações necessárias com fluent validator

        //TODO - Realizar teste unitario do order

        //TODO - Realizar o Teste com where

        //TODO - Realizar teste dos handlers 

        //TODO - Realizar teste das controllers
    }
}
