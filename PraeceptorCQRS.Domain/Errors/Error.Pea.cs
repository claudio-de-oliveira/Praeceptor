using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class Pea
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "Pea.DataBaseError",
                    description: "Problemas com o banco de dados.");

            public static ErrorOr.Error DuplicateCode
                => ErrorOr.Error.Conflict(
                    code: "Pea.DuplicateCode",
                    description: "Código já existe.");

            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "Pea.NotFound",
                    description: "Componente não encontrado.");

            public static ErrorOr.Error InvalidSyntax
                => ErrorOr.Error.NotFound(
                    code: "Pea.InvalidSyntax",
                    description: "Sintaxe inválida.");

            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "Pea.Canceled",
                    description: "Operação cancelada.");
        }
    }
}