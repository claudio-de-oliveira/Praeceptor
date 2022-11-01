using Mapster;

using PraeceptorCQRS.Application.Entities.Node.Commands;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Contracts.Entities.Node;

namespace PraeceptorCQRS.Presentation.Document.Api.Common.Mapping
{
    public class NodeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateFirstNodeRequest, CreateFirstNodeCommand>();
            config.NewConfig<InsertNodeRequest, InsertNodeBeforeCommand>();
            config.NewConfig<InsertNodeRequest, InsertNodeAfterCommand>();
            config.NewConfig<NodeResult, NodeResponse>()
                .Map(dest => dest, src => src.Node);
        }
    }
}
