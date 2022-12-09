using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public record GetSqlDocxStreamByCodeQueryXXX(
        Guid InstituteId,
        string Code
        ) : IRequest<ErrorOr<DocxResult>>;
}