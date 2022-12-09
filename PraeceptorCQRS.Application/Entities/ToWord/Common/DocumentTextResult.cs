namespace PraeceptorCQRS.Application.Entities.ToWord.Common;

public record DocumentTextResult(
    Guid DocumentId,
    string Title,
    string Text
    );