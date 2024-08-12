namespace Service.Abstractions.Models;
public class BaseModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.Now;
}