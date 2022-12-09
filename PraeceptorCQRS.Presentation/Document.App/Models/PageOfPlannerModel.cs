namespace Document.App.Models;

public record PageOfPlannerModel(
    int CurrentPage,
    int Size,
    int PreviousPage,
    int NextPage,
    int NumberOfPages,
    List<PlannerModel> Entities
    );