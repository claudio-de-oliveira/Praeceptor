namespace PraeceptorCQRS.Contracts.Entities.SimpleTable;

public record SimpleTableResponse(
    Guid Id,
    string Code,
    string Title,
    string Header,
    string Rows,
    string? Footer,
    Guid InstituteId,
    DateTime Created,
    string? CreatedBy,
    DateTime? LastModified,
    string? LastModifiedBy
    );