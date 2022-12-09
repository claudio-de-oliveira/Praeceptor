using FluentValidation;

using PraeceptorCQRS.Application.Entities.Course.Commands;
using PraeceptorCQRS.Application.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.CreateCommand
{
    public class CreateSimpleTableCommandValidator : AbstractValidator<CreateSimpleTableCommand>
    {
        public CreateSimpleTableCommandValidator(ISimpleTableRepository tableRepository, IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(250)
                // code must be unique
                .MustAsync(async (code, cancellation) =>
                {
                    bool exists = await tableRepository.Exists(o => o.Code == code);
                    return !exists;
                });

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Header)
                .NotEmpty();

            // institute must exist
            RuleFor(x => x.InstituteId)
                .MustAsync(async (instituteId, cancellation) =>
                {
                    bool exists = await instituteRepository.Exists(o => o.Id == instituteId);
                    return exists;
                });
        }
    }
}