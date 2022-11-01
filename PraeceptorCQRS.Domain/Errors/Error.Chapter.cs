using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class Chapter
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "Chapter.DataBaseError",
                    description: "Problemas com o banco de dados.");
            public static ErrorOr.Error DuplicateCode
                => ErrorOr.Error.Conflict(
                    code: "Chapter.DuplicateCode",
                    description: "Código já existe.");
            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "Chapter.NotFound",
                    description: "Componente não encontrado.");
            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "Chapter.Canceled",
                    description: "Operação cancelada.");
        }
    }
}

