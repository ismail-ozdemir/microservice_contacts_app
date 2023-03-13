using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;

namespace ContactService.Application.UnitTest.Features.PersonFeatures.Commands
{
    public class AddPersonCommand_UnitTest
    {

        [Test]
        public void AddPersonCommand_CreateWithCreatePersonRequest_Success()
        {
            var req = new CreatePersonRequest();

            AddPersonCommand command = new(req);
            Assert.Pass("AddPersonCommand Created Succesfull");
        }
        [Test]
        public void AddPersonCommand_CreateNullCreatePersonRequest_ArgumentNullException()
        {
            var ex = Assert.Catch(() => { AddPersonCommand command = new(null); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentNullException)), $"beklenen hata türü {nameof(ArgumentNullException)}");
        }
    }
}
