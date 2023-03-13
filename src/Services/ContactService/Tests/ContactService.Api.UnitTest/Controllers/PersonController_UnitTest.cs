
namespace ContactService.Api.UnitTest.Controllers
{
    public class PersonController_UnitTest
    {



        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task PersonController_AddAsyncValidaData_CreatePersonResponse()
        {

            Assert.Fail("IMediator kullanımına geçildi. Ona uygun test yazılmalı.");
        }

        [Test]
        public async Task PersonController_AddAsyncNotValidaData_BadRequest()
        {

            Assert.Fail("Test yazılmadı");
        }


    }
}
