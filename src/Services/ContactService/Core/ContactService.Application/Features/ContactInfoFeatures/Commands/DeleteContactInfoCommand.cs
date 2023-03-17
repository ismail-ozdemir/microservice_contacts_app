using ContactService.Application.Exceptions;
using ContactService.Application.Interfaces.Repository;
using MediatR;

namespace ContactService.Application.Features.ContactInfoFeatures
{
    public class DeleteContactInfoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        internal class DeleteContactInfoCommandHandler : IRequestHandler<DeleteContactInfoCommand, bool>
        {

            private readonly IContactInfoRepository _contRepo;
            public DeleteContactInfoCommandHandler(IContactInfoRepository contactRepository)
            {
                _contRepo = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            }

            public async Task<bool> Handle(DeleteContactInfoCommand request, CancellationToken cancellationToken)
            {

                var contactInfo = await _contRepo.GetByIdAsync(request.Id);
                if (contactInfo == null)
                    throw new RecordNotFoundException($"{request.Id}'a ait kayıt bulunamadı.");
                await _contRepo.RemoveAsync(contactInfo);
                return true;
            }
        }
    }
}
