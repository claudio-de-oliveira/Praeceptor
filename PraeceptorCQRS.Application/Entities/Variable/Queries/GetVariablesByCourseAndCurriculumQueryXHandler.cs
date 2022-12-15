using DocumentFormat.OpenXml.Bibliography;

using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class GetVariablesByCourseAndCurriculumQueryXHandler
        : IRequestHandler<GetVariablesByCourseAndCurriculumQueryX, ErrorOr<VariableXListResult>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IVariableXRepository _variableRepository;

        public GetVariablesByCourseAndCurriculumQueryXHandler(IVariableXRepository variableRepository, ICourseRepository courseRepository)
        {
            _variableRepository = variableRepository;
            _courseRepository = courseRepository;
        }

        public async Task<ErrorOr<VariableXListResult>> Handle(GetVariablesByCourseAndCurriculumQueryX request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var course = await _courseRepository.GetCourseById(request.Id);
            if (course is null)
                return Domain.Errors.Error.Course.NotFound;

            var list = await _variableRepository.GetVariablesByGroupId(request.Id, request.Curriculum);
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@CODIGO",
                    Value = course.Code,
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@NOME",
                    Value = course.Name,
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@AC",
                    Value = course.AC.ToString(),
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@PERIODOS",
                    Value = course.NumberOfSeasons.ToString(),
                    IsDeletable = false
                }
            );
            if (!string.IsNullOrWhiteSpace(course.Telephone))
            {
                list.Add(
                    new Domain.Entities.VariableX()
                    {
                        Id = Guid.NewGuid(),
                        GroupName = "@CURSO",
                        GroupId = course.Id,
                        Curriculum = request.Curriculum,
                        VariableName = "@TELEFONE",
                        Value = course.Telephone,
                        IsDeletable = false
                    }
                );
            }
            if (!string.IsNullOrWhiteSpace(course.Email))
            {
                list.Add(
                    new Domain.Entities.VariableX()
                    {
                        Id = Guid.NewGuid(),
                        GroupName = "@CURSO",
                        GroupId = course.Id,
                        Curriculum = request.Curriculum,
                        VariableName = "@EMAIL",
                        Value = course.Email,
                        IsDeletable = false
                    }
                );
            }

            return new VariableXListResult(list);
        }
    }
}
