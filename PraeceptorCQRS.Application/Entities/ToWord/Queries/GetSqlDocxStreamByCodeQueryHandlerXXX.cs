using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

// namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
// {
//     public class GetSqlDocxStreamByCodeQueryHandler
//         : IRequestHandler<GetSqlDocxStreamByCodeQuery, ErrorOr<DocxResult>>
//     {
//         private readonly IDocxStreamRepository _repository;
//
//         public GetSqlDocxStreamByCodeQueryHandler(IDocxStreamRepository repository)
//         {
//             _repository = repository;
//         }
//
//         public async Task<ErrorOr<DocxResult>> Handle(GetSqlDocxStreamByCodeQuery request, CancellationToken cancellationToken)
//         {
//             var entity = await _repository.ReadDocx(request.InstituteId, request.Code);
//
//             if (entity is null)
//                 return Domain.Errors.Error.Docx.NotFound;
//
//             return new DocxResult(entity);
//         }
//     }
// }