using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.CreateCommand
{
    public record CreateSimpleTableCommand(
        string Code,
        string Title,
        string Header,
        string Rows,
        string Footer,
        Guid InstituteId
        ) : IRequest<ErrorOr<SimpleTableResult>>;
}