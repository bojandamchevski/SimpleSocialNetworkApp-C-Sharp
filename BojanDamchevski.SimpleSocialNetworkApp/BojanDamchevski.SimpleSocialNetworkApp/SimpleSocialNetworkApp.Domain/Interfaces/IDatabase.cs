using SimpleSocialNetworkApp.Domain.Models;
using System.Collections.Generic;

namespace SimpleSocialNetworkApp.Domain.Interfaces
{
    public interface IDatabase
    {
        List<User> GetAll();
        User GetbyId(int id);
        int Insert(User entity);
        void Update(User entity);
        void RemoveById(int id);
    }
}
