namespace Document.App.Models;

public record BibItemValueModel(
    Guid Id,
    ListOfNomesModel Autores,
    ListOfNomesModel Tradutores,
    ListOfNomesModel Organizadores,
    string? Editor,
    int Exemplares,
    string? Title,
    PublisherValueModel? Publisher,
    int Year,
    VolumeValueModel? Volume,
    string? Series,
    int Edition,
    string? Note,
    string? ISBN,
    bool Online,
    string? Details
    );