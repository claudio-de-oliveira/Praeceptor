using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public record DeleteInstituteCommand(
        Guid Id
        ) : IRequest<ErrorOr<InstituteResult>>;
}

