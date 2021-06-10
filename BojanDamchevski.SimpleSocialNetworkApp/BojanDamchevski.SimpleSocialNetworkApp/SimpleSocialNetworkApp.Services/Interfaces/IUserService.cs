using SimpleSocialNetworkApp.Domain.Models;

namespace SimpleSocialNetworkApp.Services.Interfaces
{
    public interface IUserService
    {
        User LogIn();
        void Register();
        void AddFriend(User user);
        void SendMessage(User user);
        void ViewMessages(User user);
        void ViewFriends(User user);
        void ChangePassword(User user);
        void ChangeUsername(User user);
        void ChangeFirstName(User user);
        void ChangeLastName(User user);
        void DeactivateAccount(User user);
        void ChangeAddressInfo(User user);
    }
}
