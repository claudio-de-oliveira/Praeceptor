using Mapster;

using PraeceptorCQRS.Application.Entities.Pea.Command;
using PraeceptorCQRS.Application.Entities.Pea.Command.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Pea;

namespace Pea.Api.Common.Mappint
{
    public class PeaMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatePeaRequest, CreatePeaCommand>();
            config.NewConfig<UpdatePeaRequest, UpdatePeaCommand>();
            config.NewConfig<PeaResult, PeaResponse>()
                .Map(dest => dest, src => src.Pea);
            config.NewConfig<PeaPageResult, PageResponse<PeaResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
