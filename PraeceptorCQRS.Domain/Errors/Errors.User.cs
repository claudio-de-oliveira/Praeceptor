using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class User
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "User.DataBaseError",
                    description: "Problemas com o banco de dados.");
            public static ErrorOr.Error DuplicateEmail
                => ErrorOr.Error.Conflict(
                    code: "User.DuplicateEmail",
                    description: "Email already in use.");
            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "User.NotFound",
                    description: "Componente não encontrado.");
            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "User.Canceled",
                    description: "Operação cancelada.");
        }
    }
}
