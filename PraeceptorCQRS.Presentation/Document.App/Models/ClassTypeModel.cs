namespace Document.App.Models;

public class ClassTypeModel : Entity
{
    public Guid InstituteId { get; set; }
    public string Code { get; set; } = null!;
}