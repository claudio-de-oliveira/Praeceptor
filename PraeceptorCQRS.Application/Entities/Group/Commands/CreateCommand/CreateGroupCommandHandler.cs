using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Commands.CreateCommand
{
    public class CreateGroupCommandHandler
        : IRequestHandler<CreateGroupCommand, ErrorOr<GroupResult>>
    {
        private readonly IGroupRepository _repository;

        public CreateGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Group()
            { 
                Code = request.Code,
                InstituteId = request.InstituteId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Group.Canceled;

            var created = await _repository.CreateGroup(entity);
            if (created is null)
                return Domain.Errors.Error.Group.DataBaseError;

            return new GroupResult(created);
        }
    }
}
