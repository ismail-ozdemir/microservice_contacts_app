using ContactService.Application.Features.ContactInfoFeatures;
using FluentValidation;

namespace ContactService.Application.Validators.ContactInfo
{
    internal class DeleteContactInfoValidator : AbstractValidator<DeleteContactInfoCommand>
    {
        public DeleteContactInfoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("silinecek kaydın id değeri boş olamaz");
        }
    }

}
 