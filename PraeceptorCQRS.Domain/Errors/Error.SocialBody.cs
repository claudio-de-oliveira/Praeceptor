using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class SocialBody
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "SocialBody.DataBaseError",
                    description: "Problemas com o banco de dados.");
            public static ErrorOr.Error DuplicateCode
                => ErrorOr.Error.Conflict(
                    code: "SocialBody.DuplicateCode",
                    description: "Código já existe.");
            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "SocialBody.NotFound",
                    description: "Componente não encontrado.");
            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "SocialBody.Canceled",
                    description: "Operação cancelada.");
        }
    }
}
