using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public class UpdateClassTypeCommandValidator
    {
        public UpdateClassTypeCommandValidator(IClassTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
        }
    }
}

