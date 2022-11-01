using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Pea.Api.Controllers
{
    [Route("Pea")]
    public class PeaController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PeaController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
