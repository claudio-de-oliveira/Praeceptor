using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Commands
{
    public record DeleteSqlDocxStreamCommand(
        Guid Id
        ) : IRequest<ErrorOr<DocxResult>>;
}