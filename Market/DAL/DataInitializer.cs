using Market.Enums;
using Market.Misc;
using Market.Models;

namespace Market.DAL;

internal static class DataInitializer
{
    private static readonly Random Random = Random.Shared;
    private static readonly ProductCategory[] Categories = Enum.GetValues<ProductCategory>();

    public static Product[] InitializeProduct(int count = 10)
    {
        return Enumerable.Range(1, count).Select(number =>
            new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Product-{number}",
                Description = $"Some description for product-{number}",
                PriceInRubles = (decimal)Random.NextDouble(100, 10000),
                Category = Categories[Random.Next(Categories.Length)],
                SellerId = Guid.NewGuid()
            })
            .ToArray();
    }

    public static Cart[] InitalizeCart()
    {
        return new Cart[]
        {
            new Cart()
            {
                CustoerId = Guid.Parse("9FB44332-F767-4CC1-BE1F-05CABA051885")
            }
        };
    }
}