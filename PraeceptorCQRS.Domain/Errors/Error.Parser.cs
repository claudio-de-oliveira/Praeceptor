namespace PraeceptorCQRS.Domain.Errors;

public partial class Error
{
    public static class Parser
    {
        public static ErrorOr.Error Syntax
            => ErrorOr.Error.Validation(
                code: "Parser.Syntax",
                description: "Problemas sintáticos.");
    }
}