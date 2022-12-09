namespace Document.App.Models;

public record PageOfClassTypeModel(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<ClassTypeModel> Entities
    );