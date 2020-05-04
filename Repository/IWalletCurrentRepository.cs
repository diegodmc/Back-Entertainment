using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IWalletCurrentRepository
    {
        List<WalletCurrent> GetAllWalletsCurrent(string codeWallet);
        List<WalletCurrent> GetWalletCurrent(string email,string codeWallet);
        void CreateWalletCurrent(string codeWallet);
        void UpdateWalletCurrent(WalletCurrent wallet);

        void UpdateWalletStart(string CodeWallet);
        
    }
}