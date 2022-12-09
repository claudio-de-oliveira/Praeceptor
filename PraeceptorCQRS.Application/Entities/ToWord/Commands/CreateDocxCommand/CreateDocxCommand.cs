using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Commands;

public record CreateDocxCommand(
    Guid CourseId,
    int Curriculum,
    string Description,
    Guid DocumentId,
    Guid TemplateId,
    Guid FileId,
    Dictionary<string, string> GroupValues,
    string CreatedBy
    ) : IRequest<ErrorOr<SqlDocxInfoResult>>;