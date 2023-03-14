using ContactService.Api.Controllers;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using System.Net;

namespace Controllers
{
    public class PersonController_UnitTest
    {

        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = GetFakeMediator();
        }


        #region Create Person Controller
        [Test]
        public void PersonController_CreatePersonControllerWithMediator_Succesfull()
        {
            PersonController c = new PersonController(_mediator);
            Assert.Pass($"{nameof(PersonController)} created successfully");
        }
        [Test]
        public void PersonController_CreatePersonControllerWithNotMediator_ArgumentNullException()
        {

            var ex = Assert.Catch(() => { PersonController c = new(null); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentNullException)), $"beklenen hata türü {nameof(ArgumentNullException)}");

        }

        #endregion

        [Test]
        public async Task PersonController_AddAsyncValidaData_CreatePersonResponse()
        {
            PersonController c = new PersonController(_mediator);
            var req = new CreatePersonCommand { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var res = await c.AddAsync(req);


            Assert.That(res.GetType(), Is.EqualTo(typeof(ObjectResult)), $"expected response type :  {nameof(ObjectResult)}");
            if (res is ObjectResult respose)
            {
                Assert.IsNotNull(respose.StatusCode, "status code boş dönmemeli.");
                Assert.That((int)respose.StatusCode!, Is.EqualTo((int)HttpStatusCode.Created));
            }
        }


        [Test]
        public async Task PersonController_RemoveAsync_OK()
        {
            PersonController c = new PersonController(_mediator);
            var command = new RemovePersonCommand { Id = Guid.NewGuid() };
            var res = await c.RemoveAsync(command);


            Assert.That(res.GetType(), Is.EqualTo(typeof(OkObjectResult)), $"expected response type :  {nameof(OkObjectResult)}");
            if (res is OkObjectResult respose)
            {
                Assert.IsNotNull(respose.StatusCode, "status code boş dönmemeli.");
                Assert.That((int)respose.StatusCode!, Is.EqualTo((int)HttpStatusCode.OK));
            }

        }




        private IMediator GetFakeMediator()
        {
            // TODO : parametreye göre dönüş değerleri doldurulacak
            var req = new CreatePersonResponseDto { PersonId = Guid.NewGuid() };
            Mock<IMediator> mediator = new Mock<IMediator>();

            mediator.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(new CreatePersonResponseDto { PersonId = Guid.NewGuid() });


            mediator.Setup(m => m.Send(It.IsAny<RemovePersonCommand>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync("success");


            return mediator.Object;
        }
        //.Returns<AddPersonCommand>((command) => Task.FromResult(new CreatePersonResponse { PersonId = Guid.NewGuid(), Name = command.data.Name, Surname = command.data.Surname, Company = command.data.Company }));



    }
}
