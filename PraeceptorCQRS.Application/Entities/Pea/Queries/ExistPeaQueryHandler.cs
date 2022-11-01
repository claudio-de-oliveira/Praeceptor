using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public class ExistPeaQueryHandler
        : IRequestHandler<ExistPeaQuery, ErrorOr<bool>>
    {
        private readonly IPeaRepository peaRepository;
        private readonly IClassRepository classRepository;

        public ExistPeaQueryHandler(IPeaRepository peaRepository, IClassRepository classRepository)
        {
            this.peaRepository = peaRepository;
            this.classRepository = classRepository;
        }

        public Task<ErrorOr<bool>> Handle(ExistPeaQuery request, CancellationToken cancellationToken)
        {
            var classCode = request.Code;


            throw new NotImplementedException();
        }
    }
}
