using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IWalletRepository
    {
        IEnumerable<Wallet> GetAllWallets();
        Wallet GetWallet(string email, string status, int codeWallet);
        void CreateWallet(Wallet wallet);
        void UpdateWallet(Wallet wallet);
        void DeleteWallet(Wallet wallet);
    }
}