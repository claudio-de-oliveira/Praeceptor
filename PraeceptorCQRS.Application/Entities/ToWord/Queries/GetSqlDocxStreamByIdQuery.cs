using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public record GetSqlDocxStreamByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<DocxResult>>;
}