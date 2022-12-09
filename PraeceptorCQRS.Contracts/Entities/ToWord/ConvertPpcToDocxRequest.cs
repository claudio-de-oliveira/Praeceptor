namespace PraeceptorCQRS.Contracts.Entities.ToWord
{
    public record ConvertPpcToDocxRequest(
        Guid CourseId,
        int Curriculum,
        string Description,
        Guid DocumentId,
        Guid TemplateId,
        Guid FileId,
        Dictionary<string, string> GroupValues,
        string CreatedBy
    );
}