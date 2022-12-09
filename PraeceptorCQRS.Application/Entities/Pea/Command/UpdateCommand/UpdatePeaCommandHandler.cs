using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.Pea.Command.UpdateCommand
{
    public class UpdatePeaCommandHandler
        : IRequestHandler<UpdatePeaCommand, ErrorOr<PeaResult>>
    {
        private readonly IPeaRepository _repository;

        public UpdatePeaCommandHandler(IPeaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PeaResult>> Handle(UpdatePeaCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Pea.Canceled;

            var pea = await _repository.GetPeaById(request.Id);

            if (pea is null)
                return Domain.Errors.Error.Pea.NotFound;

            Parser parser = new();
            var updated = await Task.Run(() => parser.Parse(request.Text, null));
            if (updated is null)
                return Domain.Errors.Error.Pea.InvalidSyntax;

            pea.Text = request.Text;

            pea.LastModified = DateTime.UtcNow;
            pea.LastModifiedBy = request.LastModifiedBy;

            await _repository.UpdatePea(pea);

            PeaModel model = (PeaModel)updated;
            model.Created = pea.Created;
            model.CreatedBy = pea.CreatedBy;
            model.LastModified = pea.LastModified;
            model.LastModifiedBy = pea.LastModifiedBy;
            model.ClassId = pea.ClassId;

            return new PeaResult(model);
        }
    }
}