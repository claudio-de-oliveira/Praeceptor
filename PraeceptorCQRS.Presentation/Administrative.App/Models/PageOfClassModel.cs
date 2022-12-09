namespace Administrative.App.Models;

public record PageOfClassModel(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<ClassModel> Entities
    );