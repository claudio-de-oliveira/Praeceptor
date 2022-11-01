using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class SubSection
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "SubSection.DataBaseError",
                    description: "Problemas com o banco de dados.");
            public static ErrorOr.Error DuplicateCode
                => ErrorOr.Error.Conflict(
                    code: "SubSection.DuplicateCode",
                    description: "Código já existe.");
            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "SubSection.NotFound",
                    description: "Componente não encontrado.");
            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "SubSection.Canceled",
                    description: "Operação cancelada.");
        }
    }
}
