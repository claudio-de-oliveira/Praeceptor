namespace Administrative.App.Models;

public record PageOfPreceptorDegreeTypeModel(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<PreceptorDegreeTypeModel> Entities
    );
