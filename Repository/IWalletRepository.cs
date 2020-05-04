using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IWalletRepository
    {
        List<Wallet> GetAllWallets(string codeWallet, string statusWallet);
        List<Wallet> GetWallet(string email);
        void CreateWallet(Wallet wallet);
        void UpdateWallet(Wallet wallet);
        
    }
}