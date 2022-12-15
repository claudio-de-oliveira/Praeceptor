using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Node.Commands;
using PraeceptorCQRS.Application.Entities.Node.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Entities.Node.Queries;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Presentation.Document.Api.Controllers;

namespace Document.Api.Controllers
{
    public class ListEndPoints : ApiController
    {
        protected readonly ISender _mediator;
        protected readonly IMapper _mapper;

        protected ListEndPoints(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected async Task<ActionResult<ErrorOr<NodeResult>>> CreateFirstNode(CreateFirstNodeRequest request)
        {
            var command = _mapper.Map<CreateFirstNodeCommand>(request);

            return await _mediator.Send(command);
        }

        protected async Task<ActionResult<ErrorOr<NodeResult>>> InsertNodeAfterPosition(InsertNodeRequest request)
        {
            var command = _mapper.Map<InsertNodeAfterCommand>(request);

            return await _mediator.Send(command);
        }

        protected async Task<ActionResult<ErrorOr<NodeResult>>> InsertNodeBeforePosition(InsertNodeRequest request)
        {
            var command = _mapper.Map<InsertNodeBeforeCommand>(request);

            return await _mediator.Send(command);
        }

        protected async Task<ErrorOr<bool>> MoveForward(Guid parentId, Guid nodeId)
            => await _mediator.Send(new MoveForwardCommand(parentId, nodeId));
        protected async Task<ErrorOr<bool>> MoveBackward(Guid parentId, Guid nodeId)
            => await _mediator.Send(new MoveBackwardCommand(parentId, nodeId));

        protected async Task<ErrorOr<NodeResult>> GetFirstNodePosition(Guid nodeId)
            => await _mediator.Send(new GetFirstNodeQuery(nodeId));
        // => await _mediator.Send(new GetFirstNodeQuery(nodeId));
        protected async Task<ErrorOr<NodeResult>> GetNextNodePosition(Guid id)
            => await _mediator.Send(new GetNextNodeQuery(id));
        protected async Task<ErrorOr<NodeResult>> GetLastNodePosition(Guid nodeId)
            => await _mediator.Send(new GetLastNodeQuery(nodeId));
        // => await _mediator.Send(new GetLastNodeQuery(nodeId));
        protected async Task<ErrorOr<NodeResult>> GetPreviousNodePosition(Guid id)
            => await _mediator.Send(new GetPreviousNodeQuery(id));
        protected async Task<ErrorOr<NodeResult>> DeleteNodeAt(Guid id)
            => await _mediator.Send(new DeleteNodeCommand(id));
    }
}
