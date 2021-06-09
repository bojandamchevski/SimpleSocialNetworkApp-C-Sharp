using System.Collections.Generic;

namespace SimpleSocialNetworkApp.Domain.Models
{
    public class Friends
    {
        public List<User> FriendsList { get; set; }
        public Friends()
        {
            FriendsList = new List<User>();
        }
    }
}
