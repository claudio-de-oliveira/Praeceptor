namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class Authentication
        {
            public static ErrorOr.Error InvalidCredentials
                => ErrorOr.Error.Validation(
                    code: "Auth.InvalidCred",
                    description: "Invalid credentials.");
        }
    }
}

