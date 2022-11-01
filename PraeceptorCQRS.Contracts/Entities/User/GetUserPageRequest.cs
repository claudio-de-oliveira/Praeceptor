namespace PraeceptorCQRS.Contracts.Entities.User
{
    public record GetUserPageRequest(
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? HoldingIdFilter,
        string? InstituteIdFilter,
        string? CourseIdFilter,
        string? UserNameFilter,
        string? EmailFilter,
        string? PhoneNumberFilter,
        bool? EnabledFilter,
        char? GenderFilter
        );
}
