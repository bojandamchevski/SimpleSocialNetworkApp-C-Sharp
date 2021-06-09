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
    }
}
