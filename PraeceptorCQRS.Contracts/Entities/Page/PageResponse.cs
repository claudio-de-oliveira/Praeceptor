namespace PraeceptorCQRS.Contracts.Entities.Page
{
    public record PageResponse<T>(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<T> Entities
        );
}
