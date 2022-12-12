using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Course.Common;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public record UpdateCourseCommand(
        Guid Id,
        string Name,
        int AC,
        int NumberOfSeasons,
        int MinimumWorkload,
        string? Telephone,
        string? Email,
        string? Image
        ) : IRequest<ErrorOr<CourseResult>>;
}

