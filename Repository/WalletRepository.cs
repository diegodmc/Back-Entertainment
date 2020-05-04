using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System;

namespace Back_Entertainment.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly string _connectionString;
        public WalletRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        public List<Wallet> GetAllWallets(string codeWallet, string statusWallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @" SELECT  CodeWallet ,"+
                                        "StatusWallet ,"+
                                        "Email ,"+
                                        "FirstAction ,"+
                                        "FirstPrcAction ,"+
                                        "FirstPctAction ,"+
                                        "SecondAction ,"+
                                        "SecondPrcAction ,"+
                                        "SecondPctAction ,"+
                                        "ThirdAction ,"+
                                        "ThirdPrcAction,"+
                                        "ThirdPctAction,"+
                                        "FourthAction ,"+
                                        "FourthPrcAction,"+
                                        "FourthPctAction,"+
                                        "FifthAction ,"+
                                        "FifthPrcAction ,"+
                                        "FifthPctAction ,"+
                                        "FifthPctAction +FourthPctAction +ThirdPctAction +SecondPctAction + FirstPctAction as balance"+
                                  " FROM WALLET "+
                                  " WHERE CodeWallet = @CodeWallet " +
                                  "   AND StatusWallet = @StatusWallet "+
                                  " ORDER BY 19 DESC";
                var result = connection.Query<Wallet>(sql, new { codeWallet,statusWallet });
                return result.AsList();
            }
        }

        public List<Wallet> GetWallet(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"SELECT  CodeWallet, "+
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
                                     "FifthPrcAction  "+
                               " FROM WALLET "+
                              " WHERE Email = @Email ";
                var result = connection.Query<Wallet>(sql, new { email });
                return result.AsList();
         }
        }
         public void CreateWallet(Wallet wallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(@"INSERT INTO WALLET ( CodeWallet ,StatusWallet ,Email ,FirstAction ,FirstPctAction ,FirstPrcAction ,SecondAction ,SecondPctAction,SecondPrcAction ,ThirdAction ,ThirdPctAction,ThirdPrcAction,FourthAction ,FourthPctAction,FourthPrcAction,FifthAction ,FifthPctAction ,FifthPrcAction ) VALUES( @CodeWallet, @StatusWallet ,@Email ,@FirstAction ,@FirstPctAction ,@FirstPrcAction ,@SecondAction ,@SecondPctAction,@SecondPrcAction ,@ThirdAction ,@ThirdPctAction,@ThirdPrcAction,@FourthAction ,@FourthPctAction,@FourthPrcAction,@FifthAction ,@FifthPctAction ,@FifthPrcAction  );",
            
                                          new {       CodeWallet    = wallet.CodeWallet    ,  
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
                                                    FifthPrcAction  = wallet.FifthPrcAction 
                                              });
                
            }
        }

         public void UpdateWallet(Wallet wallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"UPDATE WALLET          SET FirstAction =@FirstAction,"+
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
                                                        "FifthPrcAction =@FifthPrcAction  "+
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
                                                    FifthPrcAction  = wallet.FifthPrcAction 
                                              });
                
            }
        }
        

         




    }

}