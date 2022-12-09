using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    // public class ExistSqlDocxStreamCodeQueryHandler
    //     : IRequestHandler<ExistSqlDocxStreamCodeQuery, ErrorOr<ExistDocxStreamResult>>
    // {
    //     private readonly IDocxStreamRepository _repository;
    //
    //     public ExistSqlDocxStreamCodeQueryHandler(IDocxStreamRepository repository)
    //     {
    //         _repository = repository;
    //     }
    //
    //     public async Task<ErrorOr<ExistDocxStreamResult>> Handle(ExistSqlDocxStreamCodeQuery request, CancellationToken cancellationToken)
    //     {
    //         if (cancellationToken.IsCancellationRequested)
    //             return Domain.Errors.Error.Docx.Canceled;
    //
    //         bool exist = await Task.Run(() => _repository.Exists(request.InstituteId, request.Code));
    //
    //         return new ExistDocxStreamResult(exist);
    //     }
    // }
}