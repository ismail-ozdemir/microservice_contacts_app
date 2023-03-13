using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Wrappers;

namespace ContactService.Application.Interfaces.Services
{
    public interface IPersonService
    {
        Task<ServiceResponse<CreatePersonResponse>> AddAsync(CreatePersonRequest request);
    }
}
