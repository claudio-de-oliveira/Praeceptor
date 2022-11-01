using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public record CreateCourseCommand(
        string Code,
        string Name,
        Guid? CEO,
        int AC,
        int NumberOfSeasons,
        int MinimumWorkload,
        string? Telephone,
        string? Email,
        string? Image,
        Guid InstituteId
        ) : IRequest<ErrorOr<CourseResult>>;
}

