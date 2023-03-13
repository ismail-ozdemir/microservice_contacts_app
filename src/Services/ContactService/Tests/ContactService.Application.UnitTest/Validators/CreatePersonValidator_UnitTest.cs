using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Validators.Person;

namespace ContactService.Application.UnitTest.Validators
{
    public class CreatePersonValidator_UnitTest
    {
        CreatePersonValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new CreatePersonValidator();
        }

        #region Name Validation Test
        [Test]
        public void CreatePersonValidator_NameEmpty_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "",
                Surname = "özdemir",
                Company = "netcad"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Name) && e.ErrorCode == "NotEmptyValidator").Any();
            Assert.IsTrue(result);
        }
        [Test]
        public void CreatePersonValidator_NameLengthLessThan3_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "is",
                Surname = "özdemir",
                Company = "netcad"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Name) && e.ErrorCode == "MinimumLengthValidator").Any();
            Assert.IsTrue(result, "Name alanı 3 karakterden kısa olamaz");
        }
        [Test]
        public void CreatePersonValidator_NameLengthMoreThan2_Valid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ism",
                Surname = "özdemir",
                Company = "netcad"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Name) && e.ErrorCode == "MinimumLengthValidator").Any();
            Assert.IsFalse(result, "Name alanı 3 karakter olabilir");
        }
        [Test]
        public void CreatePersonValidator_NameLengthMoreThan100_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
                Surname = "özdemir",
                Company = "netcad"
            };

            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Name) && e.ErrorCode == "MaximumLengthValidator").Any();
            Assert.IsTrue(result, "Name alanı 100 karakterden fazla olamaz");
        }
        [Test]
        public void CreatePersonValidator_NameMaxLength100_Valid()
        {
            var req = new CreatePersonRequest
            {
                Name = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
                Surname = "özdemir",
                Company = "netcad"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Name) && e.ErrorCode == "MaximumLengthValidator").Any();
            Assert.IsFalse(result, "Name alanı 100 karakterden uzun olamaz.");
        }
        #endregion

        #region Surname Validation Test
        [Test]
        public void CreatePersonValidator_SurnameEmpty_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ismail",
                Surname = "",
                Company = "netcad"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Surname) && e.ErrorCode == "NotEmptyValidator").Any();
            Assert.IsTrue(result);
        }

        [Test]
        public void CreatePersonValidator_SurnameLengthMoreThan100_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ismail",
                Surname = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
                Company = "netcad"
            };

            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Surname) && e.ErrorCode == "MaximumLengthValidator").Any();
            Assert.IsTrue(result, $"{nameof(CreatePersonRequest.Surname)} alanı 100 karakterden fazla olamaz");
        }
        [Test]
        public void CreatePersonValidator_SurnameMaxLength100_Valid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ismail",
                Surname = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
                Company = "netcad"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Surname) && e.ErrorCode == "MaximumLengthValidator").Any();
            Assert.IsFalse(result, $"{nameof(CreatePersonRequest.Surname)} alanı 100 karakterden uzun olamaz.");
        }
        #endregion

        #region Company Validation Test
        [Test]
        public void CreatePersonValidator_CompanyEmpty_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ismail",
                Surname = "özdemir",
                Company = ""
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Company) && e.ErrorCode == "NotEmptyValidator").Any();
            Assert.IsTrue(result);
        }

        [Test]
        public void CreatePersonValidator_CompanyLengthMoreThan100_NotValid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ismail",
                Surname = "özdemir",
                Company = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901"
            };

            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Company) && e.ErrorCode == "MaximumLengthValidator").Any();
            Assert.IsTrue(result, $"{nameof(CreatePersonRequest.Company)} alanı 100 karakterden fazla olamaz");
        }
        [Test]
        public void CreatePersonValidator_CompanyMaxLength100_Valid()
        {
            var req = new CreatePersonRequest
            {
                Name = "ismail",
                Surname = "özdemir",
                Company = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
            };
            var validate = validator.Validate(req);
            bool result = validate.Errors.Where(e => e.PropertyName == nameof(CreatePersonRequest.Company) && e.ErrorCode == "MaximumLengthValidator").Any();
            Assert.IsFalse(result, $"{nameof(CreatePersonRequest.Company)} alanı 100 karakterden uzun olamaz.");
        }
        #endregion

    }
}
