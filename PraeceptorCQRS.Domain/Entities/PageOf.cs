namespace PraeceptorCQRS.Domain.Entities
{
    public record PageOf<T>(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<T> Entities
        );
}
