using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Preceptor.Common;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public record DeletePreceptorCommand(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorResult>>;
}

