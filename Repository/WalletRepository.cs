using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly string _connectionString;
        public WalletRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        public IEnumerable<Wallet> GetAllWallets()
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<Wallet>("SELECT  CodeWallet "+
                                                        "StatusWallet ,"+
                                                        "Email ,"+
                                                        "FirstAction ,"+
                                                        "FirstPctAction ,"+
                                                        "FirstPrcAction ,"+
                                                        "SecondAction ,"+
                                                        "SecondPctAction,"+
                                                        "SecondPrcAction ,"+
                                                        "ThirdAction ,"+
                                                        "ThirdPctAction,"+
                                                        "ThirdPrcAction,"+
                                                        "FourthAction ,"+
                                                        "FourthPctAction,"+
                                                        "FourthPrcAction,"+
                                                        "FifthAction ,"+
                                                        "FifthPctAction ,"+
                                                        "FifthPrtAction  FROM WALLET ");
            }
        }

        public Wallet GetWallet(string email, string status, int codeWallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {

                return  connection.Query<Wallet>("SELECT  CodeWallet "+
                                                        "StatusWallet ,"+
                                                        "Email ,"+
                                                        "FirstAction ,"+
                                                        "FirstPctAction ,"+
                                                        "FirstPrcAction ,"+
                                                        "SecondAction ,"+
                                                        "SecondPctAction,"+
                                                        "SecondPrcAction ,"+
                                                        "ThirdAction ,"+
                                                        "ThirdPctAction,"+
                                                        "ThirdPrcAction,"+
                                                        "FourthAction ,"+
                                                        "FourthPctAction,"+
                                                        "FourthPrcAction,"+
                                                        "FifthAction ,"+
                                                        "FifthPctAction ,"+
                                                        "FifthPrtAction  FROM WALLET ").AsList().Find(e => e.Email == email && e.StatusWallet == status && e.CodeWallet == codeWallet) ;
            }
        }

         public void CreateWallet(Wallet wallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO WALLET (       CodeWallet "+
                                                        "StatusWallet ,"+
                                                        "Email ,"+
                                                        "FirstAction ,"+
                                                        "FirstPctAction ,"+
                                                        "FirstPrcAction ,"+
                                                        "SecondAction ,"+
                                                        "SecondPctAction,"+
                                                        "SecondPrcAction ,"+
                                                        "ThirdAction ,"+
                                                        "ThirdPctAction,"+
                                                        "ThirdPrcAction,"+
                                                        "FourthAction ,"+
                                                        "FourthPctAction,"+
                                                        "FourthPrcAction,"+
                                                        "FifthAction ,"+
                                                        "FifthPctAction ,"+
                                                        "FifthPrtAction  )"+
                                                        " ) VALUES( "+
                                                        "@CodeWallet "+
                                                        "@StatusWallet ,"+
                                                        "@Email ,"+
                                                        "@FirstAction ,"+
                                                        "@FirstPctAction ,"+
                                                        "@FirstPrcAction ,"+
                                                        "@SecondAction ,"+
                                                        "@SecondPctAction,"+
                                                        "@SecondPrcAction ,"+
                                                        "@ThirdAction ,"+
                                                        "@ThirdPctAction,"+
                                                        "@ThirdPrcAction,"+
                                                        "@FourthAction ,"+
                                                        "@FourthPctAction,"+
                                                        "@FourthPrcAction,"+
                                                        "@FifthAction ,"+
                                                        "@FifthPctAction ,"+
                                                        "@FifthPrtAction  );";

                connection.Execute(sql, new {       CodeWallet      = wallet.CodeWallet     ,  
                                                    StatusWallet    = wallet.StatusWallet   ,  
                                                    Email           = wallet.Email          ,  
                                                    FirstAction     = wallet.FirstAction    ,  
                                                    FirstPctAction  = wallet.FirstPctAction ,  
                                                    FirstPrcAction  = wallet.FirstPrcAction ,  
                                                    SecondAction    = wallet.SecondAction   ,  
                                                    SecondPctAction = wallet.SecondPctAction,  
                                                    SecondPrcAction = wallet.SecondPrcAction,  
                                                    ThirdAction     = wallet.ThirdAction    ,  
                                                    ThirdPctAction  = wallet.ThirdPctAction ,  
                                                    ThirdPrcAction  = wallet.ThirdPrcAction ,  
                                                    FourthAction    = wallet.FourthAction   ,  
                                                    FourthPctAction = wallet.FourthPctAction,  
                                                    FourthPrcAction = wallet.FourthPrcAction,  
                                                    FifthAction     = wallet.FifthAction    ,  
                                                    FifthPctAction  = wallet.FifthPctAction ,  
                                                    FifthPrtAction  = wallet.FifthPrtAction 
                                              });
                
            }
        }

         public void UpdateWallet(Wallet wallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "UPDATE WALLET          SET FirstAction =@FirstAction,"+
                                                        "FirstPctAction =@FirstPctAction ,"+
                                                        "FirstPrcAction =@FirstPrcAction,"+
                                                        "SecondAction =@SecondAction ,"+
                                                        "SecondPctAction =@SecondPctAction,"+
                                                        "SecondPrcAction =@SecondPrcAction ,"+
                                                        "ThirdAction =@ThirdAction ,"+
                                                        "ThirdPctAction = @ThirdPctAction,"+
                                                        "ThirdPrcAction = @ThirdPrcAction,"+
                                                        "FourthAction =@FourthAction ,"+
                                                        "FourthPctAction =@FourthPctAction,"+
                                                        "FourthPrcAction =@FourthPrcAction,"+
                                                        "FifthAction=@FifthAction ,"+
                                                        "FifthPctAction=@FifthPctAction ,"+
                                                        "FifthPrtAction =@FifthPrtAction  "+
                                                " WHERE Email =  @Email "+
                                                "   AND StatusWallet = @StatusWallet "+
                                                "   AND CodeWallet = @CodeWallet ;";
                                               

                connection.Execute(sql, new {       CodeWallet      = wallet.CodeWallet     ,  
                                                    StatusWallet    = wallet.StatusWallet   ,  
                                                    Email           = wallet.Email          ,  
                                                    FirstAction     = wallet.FirstAction    ,  
                                                    FirstPctAction  = wallet.FirstPctAction ,  
                                                    FirstPrcAction  = wallet.FirstPrcAction ,  
                                                    SecondAction    = wallet.SecondAction   ,  
                                                    SecondPctAction = wallet.SecondPctAction,  
                                                    SecondPrcAction = wallet.SecondPrcAction,  
                                                    ThirdAction     = wallet.ThirdAction    ,  
                                                    ThirdPctAction  = wallet.ThirdPctAction ,  
                                                    ThirdPrcAction  = wallet.ThirdPrcAction ,  
                                                    FourthAction    = wallet.FourthAction   ,  
                                                    FourthPctAction = wallet.FourthPctAction,  
                                                    FourthPrcAction = wallet.FourthPrcAction,  
                                                    FifthAction     = wallet.FifthAction    ,  
                                                    FifthPctAction  = wallet.FifthPctAction ,  
                                                    FifthPrtAction  = wallet.FifthPrtAction 
                                              });
                
            }
        }


         public void DeleteWallet(Wallet wallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "DELETE FROM WALLET  WHERE Email =  @Email "+
                                                "   AND StatusWallet = @StatusWallet "+
                                                "   AND CodeWallet = @CodeWallet ;";
                                               

                connection.Execute(sql, new {       CodeWallet      = wallet.CodeWallet     ,  
                                                    StatusWallet    = wallet.StatusWallet   ,  
                                                    Email           = wallet.Email         
                                              });
                
            }
        }
    }

}