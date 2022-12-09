namespace PraeceptorCQRS.Contracts.Entities.SimpleTable;

public record UpdateSimpleTableRequest(
    Guid Id,
    string Title,
    string Header,
    string Rows,
    string? Footer
    );