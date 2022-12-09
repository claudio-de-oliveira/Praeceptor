namespace Administrative.App.Models
{
    public record PageOfSocialBodyEntryModel(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<SocialBodyEntryModel> Entities
        );
}
