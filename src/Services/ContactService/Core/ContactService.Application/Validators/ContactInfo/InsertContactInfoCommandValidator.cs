using ContactService.Application.Features.ContactInfoFeatures;
using ContactService.Domain;
using FluentValidation;

namespace ContactService.Application.Validators.ContactInfo
{
    internal class InsertContactInfoCommandValidator : AbstractValidator<InsertContactInfoCommand>
    {
        public InsertContactInfoCommandValidator()
        {
            RuleFor(p => p.@params.PersonId).NotEmpty().NotNull().WithMessage("Personel bilgisi boş olamaz");
            RuleFor(p => p.@params.InfoType).NotEmpty().NotNull().WithMessage("Iletişm bilgisi tipi boş olamaz")
                  .Must(m => m == InformationType.Phone.ToString() || m == InformationType.Location.ToString() || m == InformationType.Email.ToString())
                  .WithMessage("Geçersiz ileitşim bilgisi tipi. Phone,Location,Email tiplerinden biri olmalı.");
            RuleFor(p => p.@params.InfoContent).NotEmpty().NotNull().WithMessage("Iletişm bilgisi içeriği boş olamaz");

        }
    }

}
