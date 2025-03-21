using Bogus;
using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Tests.Repositories.Products
{
    public class ProductRepositoryFaker
    {
        public static int GetRandomProductId(int max = 1)
        {
            var faker = new Faker();

            return faker.Random.Int(1, max);
        }

        public static List<Product> CreateProductFaker(int count)
        {
            return new Faker<Product>("pt_BR")
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.Title, f => f.Commerce.ProductName())
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price(decimals: 2)))
                .RuleFor(x => x.Category, f => f.Commerce.Categories(f.Random.Int(1, 5)).First())
                .RuleFor(x => x.Image, f => f.Image.ToString())
                .RuleFor(x => x.UpdatedAt, DateTime.Now)
                .RuleFor(x => x.CreatedAt, DateTime.Now)
                .RuleFor(x => x.Rating, f => CreateRatingFaker(f))
                .Generate(count);
        }

        private static Rating? CreateRatingFaker(Faker f)
        {
            return f.Make(1, () => new Faker<Rating>("pt_BR")
                .RuleFor(x => x.Count, f => f.Random.Int(0, 5))
                .RuleFor(x => x.Rate, f => f.Random.Decimal(0, 5))
                .RuleFor(x => x.UpdatedAt, DateTime.Now)
                .RuleFor(x => x.CreatedAt, DateTime.Now)
                .Generate())
                .FirstOrDefault();
        }
    }
}
