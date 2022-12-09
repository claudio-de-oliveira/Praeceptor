namespace Administrative.App.Models;

public record PageOfPreceptorRegimeTypeModel(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<PreceptorRegimeTypeModel> Entities
    );
