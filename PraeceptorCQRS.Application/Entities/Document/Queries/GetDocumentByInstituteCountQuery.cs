using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Document.Common;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public record GetDocumentByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<DocumentCountResult>>;
}
