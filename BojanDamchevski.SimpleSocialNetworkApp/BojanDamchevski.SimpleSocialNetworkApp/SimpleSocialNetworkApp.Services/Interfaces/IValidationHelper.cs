using SimpleSocialNetworkApp.Domain.Models;

namespace SimpleSocialNetworkApp.Services.Interfaces
{
    public interface IValidationHelper
    {
        bool ValidateUsername(User user);
        bool ValidatePassword(User user);
        bool ValidateFirstNameLastName(User user);
        bool ValidateAge(User user);
        bool OtherValidations(User user);
    }
}
