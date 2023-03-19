using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Validators.Person;
using FluentValidation.TestHelper;

namespace Validators.PersonValidators
{
    public class RemovePersonValidator_UnitTest
    {
        RemovePersonValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new RemovePersonValidator();
        }

        [Test]
        public void RemovePersonValidator_IdEmpty_NotValid()
        {
            var req = new RemovePersonCommand { Id = Guid.Empty };
            var validate = validator.TestValidate(req);
            validate.ShouldHaveValidationErrorFor(p => p.Id).WithErrorCode("NotEmptyValidator");
        }



    }
}
