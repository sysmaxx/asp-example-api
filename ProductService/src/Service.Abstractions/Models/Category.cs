namespace Service.Abstractions.Models;
public class Category
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; }
}