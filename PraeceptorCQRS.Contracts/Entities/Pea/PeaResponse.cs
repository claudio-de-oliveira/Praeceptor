using PraeceptorCQRS.Contracts.Values;

namespace PraeceptorCQRS.Contracts.Entities.Pea;

public record PeaResponse(
    Guid Id,
    string? Ementa,
    List<string> Objetivos,
    string? Procedimentos,
    string? Avaliacao,
    List<ConceptKey> Unidade1,
    List<ConceptKey> Unidade2,
    List<BibItemValue> BibliografiaBasica,
    List<BibItemValue> BibliografiaComplementar,
    Guid ClassId,
    string DisciplinaId,

    DateTime Created,
    string? CreatedBy,
    DateTime? LastModified,
    string? LastModifiedBy
    );