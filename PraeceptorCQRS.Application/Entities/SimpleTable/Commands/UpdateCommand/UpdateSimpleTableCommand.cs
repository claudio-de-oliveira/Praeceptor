using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.UpdateCommand
{
    public record UpdateSimpleTableCommand(
        Guid Id,
        string Title,
        string Header,
        string Rows,
        string Footer
        ) : IRequest<ErrorOr<SimpleTableResult>>;
}