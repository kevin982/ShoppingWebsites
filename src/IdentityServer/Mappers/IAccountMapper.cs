using IdentityMicroservice.Models;

namespace IdentityMicroservice.Mappers
{
    public interface IAccountMapper
    {
        ApplicationUser MapSignUpRequestModelToDomain(SignUpRequestModel model);
    }
}