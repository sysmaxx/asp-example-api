namespace Service.Abstractions.Dtos;
public class ProductDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public double Price { get; set; }
    public DateTimeOffset Created {  get; set; } 
}
