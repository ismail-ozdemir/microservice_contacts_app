using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Shared.Dto.PersonDtos;
using FluentValidation;

namespace ContactService.Application.Validators.Person
{
    internal class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonValidator()
        {
            RuleFor(p => p.request.Name)
                         .NotNull().NotEmpty().WithMessage($"alan boş olamaz.")
                         .MinimumLength(3).WithMessage("3 karakterden daha uzun olmalı.")
                         .MaximumLength(100).WithMessage("maksimum uzunluk 100 karakteri geçmemelidir.")
                         .OverridePropertyName(nameof(CreatePersonRequest.Name));

            RuleFor(p => p.request.Surname)
                         .NotEmpty().NotNull().WithMessage("alan boş olamaz.")
                         .MaximumLength(100).WithMessage("maksimum uzunluk 100 karakteri geçmemelidir.")
                         .OverridePropertyName(nameof(CreatePersonRequest.Surname));

            RuleFor(p => p.request.Company)
                         .NotEmpty().NotNull().WithMessage("alan boş olamaz.")
                         .MaximumLength(100).WithMessage("maksimum uzunluk 100 karakteri geçmemelidir.")
                         .OverridePropertyName(nameof(CreatePersonRequest.Company));
        }
    }
}
