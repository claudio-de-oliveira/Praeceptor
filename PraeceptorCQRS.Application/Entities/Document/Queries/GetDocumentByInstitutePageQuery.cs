using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Document.Common;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public record GetDocumentByInstitutePageQuery(
        Guid InstituteId,
        int PageStart,
        int PageSize
        ) : IRequest<ErrorOr<DocumentListResult>>;
}
