using ContactService.Application.Dto.PersonDto;
using FluentValidation;

namespace ContactService.Application.Validators.Person
{
    internal class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonValidator()
        {
            RuleFor(p => p.Name)
                         .NotEmpty().NotNull().WithMessage("alan boş olamaz.")
                         .MinimumLength(3).WithMessage("3 karakterden daha uzun olmalı.")
                         .MaximumLength(100).WithMessage("maksimum uzunluk 100 karakteri geçmemelidir.");

            RuleFor(p => p.Surname)
                         .NotEmpty().NotNull().WithMessage("alan boş olamaz.")
                         .MaximumLength(100).WithMessage("maksimum uzunluk 100 karakteri geçmemelidir.");

            RuleFor(p => p.Company)
                         .NotEmpty().NotNull().WithMessage("alan boş olamaz.")
                         .MaximumLength(100).WithMessage("maksimum uzunluk 100 karakteri geçmemelidir.");
        }
    }
}
