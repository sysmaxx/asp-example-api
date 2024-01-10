namespace Service.Abstractions.Dtos;
public class CategoryDto
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    public string Description { get; set; }
}
