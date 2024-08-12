using Service.Abstractions.Models;

namespace Service.Api.Infrasturcture;
public static class ProductDbContextExtensions
{
    public static void SeedData(this ProductDbContext context)
    {
        if (!context.Products.Any())
        {
            var sweetsCategory = new Category { Name = "Süßwaren", Description = "Leckere Snacks und Süßigkeiten.", Created = GenerateRandomDateTimeOffset(new DateTime(2022, 07, 01), DateTime.Now) };
            var cigarettesCategory = new Category { Name = "Zigaretten", Description = "Verschiedene Tabakprodukte.", Created = GenerateRandomDateTimeOffset(new DateTime(2022, 07, 01), DateTime.Now) };
            var alcoholCategory = new Category { Name = "Alkohol", Description = "Verschiedene alkoholische Getränke.", Created = GenerateRandomDateTimeOffset(new DateTime(2022, 07, 01), DateTime.Now) };

            context.Categories.AddRange(sweetsCategory, cigarettesCategory, alcoholCategory);
            context.SaveChanges();

            var products = new List<Product>
            {
                new() { Name = "Schokolade", Description = "Vollmilchschokolade, 100g", Category = sweetsCategory, Price = 1.50, IsEnable = true, Created = GenerateRandomDateTimeOffset(new DateTime(2022,07,01), DateTime.Now) },
                new() { Name = "Gummibärchen", Description = "Bunte Fruchtgummis, 200g", Category = sweetsCategory, Price = 1.20, IsEnable = true, Created = GenerateRandomDateTimeOffset(new DateTime(2022,07,01), DateTime.Now) },
                new() { Name = "Marlboro", Description = "Marlboro Red, 20 Stk.", Category = cigarettesCategory, Price = 7.00, IsEnable = true, Created = GenerateRandomDateTimeOffset(new DateTime(2022,07,01), DateTime.Now) },
                new() { Name = "Lucky Strike", Description = "Lucky Strike Original, 20 Stk.", Category = cigarettesCategory, Price = 6.50, IsEnable = true, Created = GenerateRandomDateTimeOffset(new DateTime(2022,07,01), DateTime.Now) },
                new() { Name = "Bier", Description = "Lagerbier, 500ml", Category = alcoholCategory, Price = 1.00, IsEnable = true, Created = GenerateRandomDateTimeOffset(new DateTime(2022,07,01), DateTime.Now) },
                new() { Name = "Wein", Description = "Rotwein, 750ml", Category = alcoholCategory, Price = 5.00, IsEnable = true, Created = GenerateRandomDateTimeOffset(new DateTime(2022,07,01), DateTime.Now) }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }

    public static DateTimeOffset GenerateRandomDateTimeOffset(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date must be earlier than end date.");

        var random = new Random();
        var range = endDate - startDate;
        var randomTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));

        return startDate + randomTimeSpan;
    }
}