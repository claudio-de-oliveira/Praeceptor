using Mapster;

using PraeceptorCQRS.Application.Entities.Pea.Command;
using PraeceptorCQRS.Application.Entities.Pea.Command.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Pea;
using PraeceptorCQRS.Contracts.Values;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Domain.Values.PeaValues;

namespace Pea.Api.Common.Mapping
{
    public class PeaMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatePeaRequest, CreatePeaCommand>();
            config.NewConfig<UpdatePeaRequest, UpdatePeaCommand>();
            config.NewConfig<PeaResult, PeaResponse>()
                .MapWith(source => MapPeaResultToPeaResponse(source));
            config.NewConfig<PeaPageResult, PageResponse<PeaResponse>>()
                .MapWith(source => MapPeaPageResultToPageResponse(source));
        }

        private static BibItemValue MapBibItemToReferenceValue(BibItem item)
        {
            var authors = new ListOfNomesValue();
            item.Autores.Nomes.ForEach(
                name => authors.Nomes.Add(new PraeceptorCQRS.Contracts.Values.NomeItem(name.Nome, name.Sobrenome))
                );

            var traductors = new ListOfNomesValue();
            item.Tradutores.Nomes.ForEach(
                name => traductors.Nomes.Add(new PraeceptorCQRS.Contracts.Values.NomeItem(name.Nome, name.Sobrenome))
                );

            var organizators = new ListOfNomesValue();
            item.Organizadores.Nomes.ForEach(
                name => organizators.Nomes.Add(new PraeceptorCQRS.Contracts.Values.NomeItem(name.Nome, name.Sobrenome))
                );

            return new BibItemValue(
                authors,
                traductors,
                organizators,
                item.Editor,
                item.Exemplares,
                item.Title,
                (item.Publisher is not null)
                    ? new PublisherValue(item.Publisher.Nome, item.Publisher.Endereco)
                    : null,
                item.Year,
                (item.Volume is not null)
                    ? new VolumeValue(item.Volume.Text1, item.Volume.Text2)
                    : null,
                item.Series,
                item.Edition,
                item.Note,
                item.ISBN,
                item.Online,
                item.Details
                );
        }

        private static PeaResponse MapPeaModelToPeaResponse(PeaModel pea)
        {
            var objetivos = new List<string>();
            pea.Objetivos.ForEach(
                o => objetivos.Add(o)
                );

            var unidade1 = new List<ConceptKey>();
            pea.Unidade1.ForEach(
                o => unidade1.Add(new ConceptKey(o.Description, o.Conteudos))
                );

            var unidade2 = new List<ConceptKey>();
            pea.Unidade2.ForEach(
                o => unidade2.Add(new ConceptKey(o.Description, o.Conteudos))
                );

            var basica = new List<BibItemValue>();
            pea.BibliografiaBasica.ForEach(
                o => basica.Add(MapBibItemToReferenceValue(o))
                );

            var complementar = new List<BibItemValue>();
            pea.BibliografiaComplementar.ForEach(
                o => complementar.Add(MapBibItemToReferenceValue(o))
                );

            return new(
                pea.Id,
                pea.Ementa,
                objetivos,
                pea.Procedimentos,
                pea.Avaliacao,
                unidade1,
                unidade2,
                basica,
                complementar,
                pea.ClassId,
                pea.DisciplinaId,
                pea.Created,
                pea.CreatedBy,
                pea.LastModified,
                pea.LastModifiedBy
                );
        }

        private static PageResponse<PeaResponse> MapPeaPageResultToPageResponse(PeaPageResult source)
        {
            var entities = new List<PeaResponse>();
            foreach (var entity in source.Page.Entities)
                entities.Add(MapPeaModelToPeaResponse(entity));

            return new PageResponse<PeaResponse>(
                source.Page.CurrentPage,
                source.Page.Size,
                source.Page.PreviousPage,
                source.Page.NextPage,
                source.Page.NumberOfPages,
                entities
                );
        }

        private static PeaResponse MapPeaResultToPeaResponse(PeaResult source)
            => MapPeaModelToPeaResponse(source.Pea);
    }
}