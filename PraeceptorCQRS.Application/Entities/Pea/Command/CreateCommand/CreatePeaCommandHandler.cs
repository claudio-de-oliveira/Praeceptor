using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.Pea.Command
{
    public class CreatePeaCommandHandler
        : IRequestHandler<CreatePeaCommand, ErrorOr<PeaResult>>
    {
        private readonly IPeaRepository _peaRepository;

        public CreatePeaCommandHandler(IPeaRepository peaRepository)
        {
            _peaRepository = peaRepository;
        }

        public async Task<ErrorOr<PeaResult>> Handle(CreatePeaCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Pea.Canceled;

            Parser parser = new();
            var result = await Task.Run(() => parser.Parse(request.Text, null));
            if (result is null)
                return Domain.Errors.Error.Pea.InvalidSyntax;

            var pea = Domain.Entities.Pea.Create(
                request.Text,
                request.ClassId,
                DateTime.UtcNow,
                request.CreatedBy
                );

            var created = await _peaRepository.CreatePea(pea);
            if (created is null)
                return Domain.Errors.Error.Pea.DataBaseError;

            PeaModel model = (PeaModel)result;
            // model.Id = pea.Id;
            model.Created = pea.Created;
            model.CreatedBy = pea.CreatedBy;
            model.LastModified = pea.LastModified;
            model.LastModifiedBy = pea.LastModifiedBy;
            model.ClassId = created.ClassId;

            return new PeaResult(model);
        }
    }
}