using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Api.Dto.ExceptionResponses
{
    [ExcludeFromCodeCoverage]
    public abstract class ExceptionResponseDtoBase
    {
        public string TypeName => this.GetType().Name;
    }

}
