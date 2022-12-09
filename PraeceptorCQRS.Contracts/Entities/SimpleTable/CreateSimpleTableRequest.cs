namespace PraeceptorCQRS.Contracts.Entities.SimpleTable;

public record CreateSimpleTableRequest(
    string Code,
    string Title,
    string? Header,
    string Rows,
    string? Footer,
    Guid InstituteId
    );