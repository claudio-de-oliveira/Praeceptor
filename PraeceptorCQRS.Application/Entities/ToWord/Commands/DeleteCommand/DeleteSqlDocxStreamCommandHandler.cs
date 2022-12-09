using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Commands
{
    public class DeleteSqlDocxStreamCommandHandler
        : IRequestHandler<DeleteSqlDocxStreamCommand, ErrorOr<DocxResult>>
    {
        private readonly IDocxStreamRepository _repository;

        public DeleteSqlDocxStreamCommandHandler(IDocxStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocxResult>> Handle(DeleteSqlDocxStreamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetDocxInfo(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Docx.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Docx.Canceled;

            await _repository.DeleteFile(request.Id);

            return new DocxResult(entity);
        }
    }
}