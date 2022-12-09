using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public class GetPeaByIdQueryHandler
        : IRequestHandler<GetPeaByIdQuery, ErrorOr<PeaResult>>
    {
        private readonly IPeaRepository _peaRepository;

        public GetPeaByIdQueryHandler(IPeaRepository peaRepository)
        {
            _peaRepository = peaRepository;
        }

        public async Task<ErrorOr<PeaResult>> Handle(GetPeaByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Pea.Canceled;

            var pea = await _peaRepository.GetPeaById(request.Id);
            if (pea is null)
                return Domain.Errors.Error.Pea.NotFound;

            Parser parser = new();
            var result = await Task.Run(() => parser.Parse(pea.Text, null));
            if (result is null)
                return Domain.Errors.Error.Pea.InvalidSyntax;

            PeaModel aux = (PeaModel)result;
            var model = new PeaModel(
                pea.Id,
                aux.Ementa,
                aux.Objetivos,
                aux.Procedimentos,
                aux.Avaliacao,
                aux.Unidade1,
                aux.Unidade2,
                aux.BibliografiaBasica,
                aux.BibliografiaComplementar,
                aux.ClassId,
                aux.DisciplinaId,
                pea.Created,
                pea.CreatedBy,
                pea.LastModified,
                pea.LastModifiedBy
                );

            return new PeaResult(model);
        }
    }
}