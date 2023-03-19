using ContactService.Application.Features.PersonFeatures.Queries;
using FluentValidation;

namespace ContactService.Application.Validators.Person
{
    internal class PersonFilterByIdValidator : AbstractValidator<GetPersonContactInfoList>
    {

        public PersonFilterByIdValidator()
        {
            RuleFor(p => p.Filter.Id).NotEmpty().NotNull().WithMessage("id alanı boş olamaz");
        }

    }

}
