namespace PraeceptorCQRS.Contracts.Entities.SqlDocxStream;

public record CreateDocxRequest(
    Guid CourseId,
    int Curriculum,
    Guid DocumentId,
    Guid TemplateId,
    Guid FileId,
    Dictionary<string, string> GroupValues,
    string CreatedBy
    );