namespace Document.App.Models;

public record PageOfSimpleTable(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<SimpleTableModel> Entities
    );