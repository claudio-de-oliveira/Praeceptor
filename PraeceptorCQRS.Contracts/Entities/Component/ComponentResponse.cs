using PraeceptorCQRS.Contracts.Entities.AxisType;
using PraeceptorCQRS.Contracts.Entities.Class;

namespace PraeceptorCQRS.Contracts.Entities.Component
{
    public record ComponentResponse(
        Guid CourseId,
        int Curriculum,
        int Season,
        Guid ClassId,
        ClassResponse Class,
        Guid AxisTypeId,
        AxisTypeResponse Axis,
        bool Optative
        );
}
