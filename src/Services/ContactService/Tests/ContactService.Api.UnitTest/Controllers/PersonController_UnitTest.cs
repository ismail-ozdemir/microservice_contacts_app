using ContactService.Api.Controllers;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Interfaces.Services;
using ContactService.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ContactService.Api.UnitTest.Controllers
{
    public class PersonController_UnitTest
    {

        IPersonService _personService;

        [SetUp]
        public void Setup()
        {
            _personService = GetPersonService();
        }

        [Test]
        public async Task PersonController_AddAsyncValidaData_CreatePersonResponse()
        {
            PersonController controller = new PersonController(_personService);

            var data = new CreatePersonRequest { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var response = await controller.AddAsync(data);



            if (response is OkObjectResult okResponse)
                Assert.IsTrue(200 == (int)okResponse.StatusCode!);
            else
                Assert.Fail();

        }
        private IPersonService GetPersonService()
        {
            var mock = new Mock<IPersonService>();
            mock.Setup(m => m.AddAsync(It.IsAny<CreatePersonRequest>()))
                      .Returns<CreatePersonRequest>(person =>
                      {
                          var data = new CreatePersonResponse
                          {
                              PersonId = Guid.NewGuid(),
                              Name = person.Name,
                              Surname = person.Surname,
                              Company = person.Company,
                          };
                          var response = new ServiceResponse<CreatePersonResponse>(data);
                          return Task.FromResult(response);
                      });
            return mock.Object;

        }

    }
}
