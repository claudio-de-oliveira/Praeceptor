namespace PraeceptorCQRS.Contracts.Entities.Pea;

public record CreatePeaRequest(
    Guid ClassId,
    string Text,
    string? CreatedBy
    );