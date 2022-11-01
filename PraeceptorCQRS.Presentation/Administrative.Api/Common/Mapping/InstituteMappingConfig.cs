using Mapster;

using PraeceptorCQRS.Application.Entities.Institute.Commands;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Entities.Institute.Queries;
using PraeceptorCQRS.Contracts.Entities.Institute;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class InstituteMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetInstitutePageRequest, GetInstitutePageQuery>();

            // config.NewConfig<CreateInstituteRequest, CreateInstituteCommand>()
            //     .Map(o => new DbKey<Holding>(o.HoldingId.Id), o => o.HoldingId);
            config.NewConfig<UpdateInstituteRequest, UpdateInstituteCommand>();
            config.NewConfig<InstituteResult, InstituteResponse>()
                .Map(dest => dest, src => src.Institute);
            config.NewConfig<InstitutePageResult, PageResponse<InstituteResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

