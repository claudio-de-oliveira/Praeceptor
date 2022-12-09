using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries;

public class GetPeasByCodeQueryHandler
    : IRequestHandler<GetPeasByCodeQuery, ErrorOr<PeaPageResult>>
{
    private readonly IClassRepository _classRepository;
    private readonly IPeaRepository _peaRepository;

    public GetPeasByCodeQueryHandler(IPeaRepository peaRepository, IClassRepository classRepository)
    {
        this._peaRepository = peaRepository;
        this._classRepository = classRepository;
    }

    public async Task<ErrorOr<PeaPageResult>> Handle(GetPeasByCodeQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.Pea.Canceled;

        var cls = await _classRepository.GetClassByCode(request.ClassCode);
        if (cls is null)
            return Domain.Errors.Error.Class.NotFound;

        var page = await _peaRepository.GetPeaPage(
            cls.Id,
            request.Start,
            request.Count,
            request.Sort,
            request.Ascending,
            request.CreatedByFilter,
            request.CreatedFilter,
            request.LastModifiedFilter,
            request.LastModifiedByFilter
            );

        if (page is null)
            return Domain.Errors.Error.Pea.NotFound;

        PageOf<PeaModel> modelList = new(
            page.CurrentPage,
            page.Size,
            page.PreviousPage,
            page.NextPage,
            page.NumberOfPages,
            new List<PeaModel>()
            );

        foreach (var entity in page.Entities)
        {
            Parser parser = new();
            var result = await Task.Run(() => parser.Parse(entity.Text, null));
            if (result is null)
                return Domain.Errors.Error.Pea.InvalidSyntax;

            PeaModel aux = (PeaModel)result;
            var model = new PeaModel(
                entity.Id,
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
                entity.Created,
                entity.CreatedBy,
                entity.LastModified,
                entity.LastModifiedBy
                );

            modelList.Entities.Add(model);
        }

        return new PeaPageResult(modelList);
    }
}