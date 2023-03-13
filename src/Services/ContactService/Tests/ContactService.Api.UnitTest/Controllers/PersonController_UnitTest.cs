
using ContactService.Api.Controllers;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ContactService.Api.UnitTest.Controllers
{
    public class PersonController_UnitTest
    {

        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = GetFakeMediator();

        }

        [Test]
        public async Task PersonController_CreatePersonControllerWithMediator_Succesfull()
        {
            PersonController c = new PersonController(_mediator);
            Assert.Pass($"{nameof(PersonController)} created successfully");
        }
        [Test]
        public void PersonController_CreatePersonControllerWithMediator_ArgumentNullException()
        {

            var ex = Assert.Catch(() => { PersonController c = new(null); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentNullException)), $"beklenen hata türü {nameof(ArgumentNullException)}");

        }


        [Test]
        public async Task PersonController_AddAsyncValidaData_CreatePersonResponse()
        {
            PersonController c = new PersonController(_mediator);
            var req = new CreatePersonRequest { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var res = await c.AddAsync(req);


            Assert.That(res.GetType(), Is.EqualTo(typeof(ObjectResult)), $"expected response type :  {nameof(ObjectResult)}");
            if (res is ObjectResult respose)
            {
                Assert.IsNotNull(respose.StatusCode, "status code boş dönmemeli.");
                Assert.That((int)respose.StatusCode!, Is.EqualTo((int)HttpStatusCode.Created));
            }
        }

        //.Returns<AddPersonCommand>((command) => Task.FromResult(new CreatePersonResponse { PersonId = Guid.NewGuid(), Name = command.data.Name, Surname = command.data.Surname, Company = command.data.Company }));

        private IMediator GetFakeMediator()
        {
            // TO DO : parametreye göre dönüş değerleri doldurulacak
            var req = new CreatePersonResponse { PersonId = Guid.NewGuid() };
            Mock<IMediator> mediator = new Mock<IMediator>();

            mediator.Setup(m => m.Send(It.IsAny<AddPersonCommand>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(new CreatePersonResponse { PersonId = Guid.NewGuid() });



            return mediator.Object;
        }


    }
}
