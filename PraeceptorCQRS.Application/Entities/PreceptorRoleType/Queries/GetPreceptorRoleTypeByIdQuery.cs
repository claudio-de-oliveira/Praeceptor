using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries
{
    public record GetPreceptorRoleTypeByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorRoleTypeResult>>;
}

