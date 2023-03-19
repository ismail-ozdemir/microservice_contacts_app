using ContactService.Application.Features.PersonFeatures.Queries;
using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Validators.Person;
using FluentValidation.TestHelper;

namespace ContactService.Application.UnitTest.Validators.PersonValidators
{
    internal class PersonFilterByIdValidator_UnitTest
    {

        PersonFilterByIdValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new PersonFilterByIdValidator();
        }

        [Test]
        public void RemovePersonValidator_IdEmpty_NotValid()
        {
            var req = new PersonFilter.ById { Id = Guid.Empty };
            var validate = validator.TestValidate(new GetPersonContactInfoList(req));
            validate.ShouldHaveValidationErrorFor(p => p.Filter.Id).WithErrorCode("NotEmptyValidator");
        }


    }
}
