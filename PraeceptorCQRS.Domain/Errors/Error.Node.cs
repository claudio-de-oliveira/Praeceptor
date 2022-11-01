using PraeceptorCQRS.Domain.Enums;

namespace PraeceptorCQRS.Domain.Errors
{
    public partial class Error
    {
        public static class Node
        {
            public static ErrorOr.Error DataBaseError
                => ErrorOr.Error.Validation(
                    code: "Node.DataBaseError",
                    description: "Problemas com o banco de dados.");
            public static ErrorOr.Error DuplicateCode
                => ErrorOr.Error.Conflict(
                    code: "Node.DuplicateCode",
                    description: "Código já existe.");
            public static ErrorOr.Error NotFound
                => ErrorOr.Error.NotFound(
                    code: "Node.NotFound",
                    description: "Componente não encontrado.");
            public static ErrorOr.Error Canceled
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "Node.Canceled",
                    description: "Operação cancelada.");
            public static ErrorOr.Error EndList
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "Node.EndList",
                    description: "Não há um próximo item.");
            public static ErrorOr.Error BeginList
                => ErrorOr.Error.Custom(
                    type: ((int)CustomErrorType.CANCELED),
                    code: "Node.BeginList",
                    description: "Não há um item anterior.");
        }
    }
}
