using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class Docx
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "Docx.DataBaseError",
                    description: "Problemas com o banco de dados.");

            public static ErrorOr.Error DuplicateCode
                => ErrorOr.Error.Conflict(
                    code: "Docx.DuplicateCode",
                    description: "Código já existe.");

            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "Docx.NotFound",
                    description: "Componente não encontrado.");

            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "Docx.Canceled",
                    description: "Operação cancelada.");
        }
    }
}