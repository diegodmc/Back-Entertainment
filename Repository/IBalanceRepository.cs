using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IBalanceRepository
    {
        List<Balance> GetAllBalances(string email);
        void CreateBalance(Balance balance);
       /* User GetUser(string email, string password);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);*/
    }
}