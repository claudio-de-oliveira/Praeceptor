namespace Administrative.App.Models;

public record PageOfPreceptorRoleTypeModel(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<PreceptorRoleTypeModel> Entities
    );
