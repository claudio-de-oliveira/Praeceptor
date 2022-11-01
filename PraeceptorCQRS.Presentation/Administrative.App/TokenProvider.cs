namespace Administrative.App
{
    public class TokenProvider
    {
        public string XsrfToken { get; set; } = default!;
    }

    public class InitialApplicationState
    {
        public string XsrfToken { get; set; } = default!;
    }
}
