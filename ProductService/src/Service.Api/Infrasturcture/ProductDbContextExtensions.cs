using Service.Abstractions.Models;

namespace Service.Api.Infrasturcture;
public static class ProductDbContextExtensions
{
    public static void SeedData(this ProductDbContext context)
    {
        if (!context.Products.Any())
        {
            var smartphoneCategory = new Category { Name = "Smartphones", Description = "Category of Smartphones" };
            var laptopCategory = new Category { Name = "Laptops", Description = "Category of Laptops" };

            context.Categories.AddRange(smartphoneCategory, laptopCategory);
            context.SaveChanges();

            var products = new List<Product>
            {
                new() { Name = "iPhone 13", Description = "Apple iPhone 13 with 128GB storage", Category = smartphoneCategory, Price = 999.99, IsEnable = true },
                new() { Name = "Samsung Galaxy S21", Description = "Samsung Galaxy S21 with 256GB storage",  Category = smartphoneCategory, Price = 899.99, IsEnable = true },
                new() { Name = "Google Pixel 6", Description = "Google Pixel 6 with 128GB storage",  Category = smartphoneCategory, Price = 799.99, IsEnable = true },
                new() { Name = "OnePlus 9", Description = "OnePlus 9 with 256GB storage", Category = smartphoneCategory, Price = 699.99, IsEnable = true },
                new() { Name = "Dell XPS 15", Description = "Dell XPS 15 with 16GB RAM and 512GB SSD", Category = laptopCategory, Price = 1500.99, IsEnable = true },
                new() { Name = "MacBook Pro 14", Description = "Apple MacBook Pro 14 with M1 chip", Category = laptopCategory, Price = 1999.99, IsEnable = true },
                new() { Name = "Lenovo ThinkPad X1 Carbon", Description = "Lenovo ThinkPad X1 Carbon with 16GB RAM and 1TB SSD", Category = laptopCategory, Price = 1400.99, IsEnable = true }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}