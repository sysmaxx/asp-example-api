namespace Service.Abstractions.Dtos;
public class CategoryDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset Created { get; set; }
}
