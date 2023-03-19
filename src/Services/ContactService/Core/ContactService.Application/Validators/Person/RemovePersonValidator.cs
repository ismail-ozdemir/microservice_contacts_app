using ContactService.Application.Features.PersonFeatures.Commands;
using FluentValidation;

namespace ContactService.Application.Validators.Person
{
    internal class RemovePersonValidator : AbstractValidator<RemovePersonCommand>
    {
        public RemovePersonValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("silinecek kaydın id değeri boş olamaz");
        }
    }

}
