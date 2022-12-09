using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public record ExistSqlDocxStreamCodeQueryXXX(
        Guid InstituteId,
        string Code
        ) : IRequest<ErrorOr<ExistDocxStreamResult>>;
}