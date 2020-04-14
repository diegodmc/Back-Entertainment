using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(string email);
        void CreateUser(User user);
       /* User GetUser(string email, string password);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);*/
    }
}