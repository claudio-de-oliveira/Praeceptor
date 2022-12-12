namespace Administrative.App.Models;

public class ClassTypeModel : AbstractTypeModel
{
    public string Code3 { get; set; } = null!;
    public Guid InstituteId { get; set; }
    public bool IsRemote { get; set; }
    public int DurationInMinutes { get; set; }
}