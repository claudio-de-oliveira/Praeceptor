namespace Administrative.App.Models;

public class ClassTypeModel : AbstractTypeModel
{
    public Guid InstituteId { get; set; }
    public bool IsRemote { get; set; }
    public int DurationInMinutes { get; set; }
}