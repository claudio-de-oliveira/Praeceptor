using PraeceptorCQRS.Contracts.Entities.ClassType;

namespace PraeceptorCQRS.Contracts.Entities.Class
{
    public record ClassResponse(
        Guid Id,
        string Code,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid InstituteId,
        Guid TypeId,
        ClassTypeResponse Type,
        bool HasPlanner,
        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}