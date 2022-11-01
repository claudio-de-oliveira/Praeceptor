namespace PraeceptorCQRS.Utilities
{
    public static class Global
    {
        public static bool MatchStringFilter(string filter, string? property)
            => !string.IsNullOrWhiteSpace(property) && property.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
        public static bool MatchIntFilter(int? filter, int? property)
            => property is not null && property == filter;
        public static bool MatchGuidFilter(Guid? filter, Guid? property)
            => property is not null && property != Guid.Empty && property == filter;
        public static bool MatchDateTimeFilter(string filter, DateTime? property)
        {
            DateTime? dt = property;
            if (dt is null)
                return false;
            return ((DateTime)dt).ToString("D").Contains(filter, StringComparison.InvariantCultureIgnoreCase);
        }
        public static List<T> SortList<T, K>(IEnumerable<T> source, Func<T, K> keySelector, bool ascending)
            => ascending ? source.OrderBy(keySelector).ToList() : source.OrderByDescending(keySelector).ToList();
        public static string ConvertDateTimeToString(DateTime? dateTime) => 
            (dateTime is not null) 
            ? Convert.ToDateTime(dateTime, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") 
            : "";
    }
}
