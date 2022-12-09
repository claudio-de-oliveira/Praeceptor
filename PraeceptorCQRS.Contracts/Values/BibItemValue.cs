namespace PraeceptorCQRS.Contracts.Values;

public record BibItemValue(
    ListOfNomesValue Autores,
    ListOfNomesValue Tradutores,
    ListOfNomesValue Organizadores,
    string? Editor,
    int Exemplares,
    string? Title,
    PublisherValue? Publisher,
    int Year,
    VolumeValue? Volume,
    string? Series,
    int Edition,
    string? Note,
    string? ISBN,
    bool Online,
    string? Details
    );