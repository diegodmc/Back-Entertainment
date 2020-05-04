using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System;

namespace Back_Entertainment.Repository
{
    public class WalletCurrentRepository : IWalletCurrentRepository
    {
        private readonly string _connectionString;
        public WalletCurrentRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }

        public void UpdateWalletStart(string CodeWallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(@"UPDATE WALLET SET STATUSWALLET = 2 WHERE CODEWALLET = @CODEWALLET AND STATUSWALLET = 1", new{CodeWallet = CodeWallet});
            }
			
        }
        public void CreateWalletCurrent(String CodeWallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(@"INSERT INTO WALLETCURRENT 
										 SELECT Id,
												CodeWallet ,
												StatusWallet ,
												Email , 
                                                now() as dateupdate,
												FirstAction , 
												FirstPctAction ,
												FirstPrcAction ,
												0,
												SecondAction ,
												SecondPctAction ,
												SecondPrcAction ,
												0,
												ThirdAction ,
												ThirdPctAction,
												ThirdPrcAction,
												0,
												FourthAction ,
												FourthPctAction,
												FourthPrcAction,
												0,
												FifthAction ,
												FifthPctAction ,
												FifthPrcAction ,
												0,
												0 as balance
										   FROM WALLET
										   WHERE CodeWallet = @CodeWallet
                                            AND StatusWallet = 2 ", 
									new{CodeWallet = CodeWallet});
            }
        }
		
		public void UpdateWalletCurrent(WalletCurrent walletCurrent)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"UPDATE WALLETCURRENT    SET FirstPctAction =@FirstPctAction ,"+
                                                        "FirstPrcActionCurrent =@FirstPrcActionCurrent,"+
														
                                                        "SecondPctAction =@SecondPctAction,"+
														"SecondPrcActionCurrent =@SecondPrcActionCurrent,"+

                                                        "ThirdPctAction = @ThirdPctAction,"+
														"ThirdPrcActionCurrent =@ThirdPrcActionCurrent,"+
                                                        
                                                        "FourthPctAction =@FourthPctAction,"+
                                                        "FourthPrcActionCurrent =@FourthPrcActionCurrent,"+
														
                                                        "FifthPctAction=@FifthPctAction ,"+
                                                        "FifthPrcActionCurrent =@FifthPrcActionCurrent"+
                                                " WHERE CodeWallet = @CodeWallet "+
                                                "  AND Email = @Email ";
                
               
                connection.Execute(sql, new {       FirstPctAction  =  WithTwoDecimalPoints((((Convert.ToDecimal(walletCurrent.FirstPrcActionCurrent)*100)/Convert.ToDecimal(walletCurrent.FirstPrcAction))-100)) , 
													FirstPrcActionCurrent = walletCurrent.FirstPrcActionCurrent.Replace(",","."),
                                                    
                                                    SecondPctAction =  WithTwoDecimalPoints((((Convert.ToDecimal(walletCurrent.SecondPrcActionCurrent)*100)/Convert.ToDecimal(walletCurrent.SecondPrcAction))-100)) , 
                                                    SecondPrcActionCurrent = walletCurrent.SecondPrcActionCurrent.Replace(",","."),
													
                                                    ThirdPctAction  =  WithTwoDecimalPoints((((Convert.ToDecimal(walletCurrent.ThirdPrcActionCurrent)*100)/Convert.ToDecimal(walletCurrent.ThirdPrcAction))-100)) , 
													ThirdPrcActionCurrent = walletCurrent.ThirdPrcActionCurrent.Replace(",","."),
                                                    
                                                    FourthPctAction =  WithTwoDecimalPoints((((Convert.ToDecimal(walletCurrent.FourthPrcActionCurrent)*100)/Convert.ToDecimal(walletCurrent.FourthPrcAction))-100)) , 
													FourthPrcActionCurrent = walletCurrent.FourthPrcActionCurrent.Replace(",","."),
                                                    
                                                    FifthPctAction  =  WithTwoDecimalPoints((((Convert.ToDecimal(walletCurrent.FifthPrcActionCurrent)*100)/Convert.ToDecimal(walletCurrent.FifthPrcAction))-100)) , 
													FifthPrcActionCurrent = walletCurrent.FifthPrcActionCurrent.Replace(",","."),
                                                    
                                                    
                                                    CodeWallet = walletCurrent.CodeWallet,
                                                    Email = walletCurrent.Email
                                              });
                sql = @"UPDATE WALLETCURRENT    SET Balance =FirstPctAction+SecondPctAction+ThirdPctAction+FourthPctAction+FifthPctAction "+
                                                " WHERE CodeWallet = @CodeWallet "+
                                                "  AND Email = @Email ";
               connection.Execute(sql, new{email = walletCurrent.Email, codeWallet = walletCurrent.CodeWallet});    
            }
        }
        
        public decimal WithTwoDecimalPoints(decimal val)
        {
            return decimal.Parse(val.ToString("0.00"));
        }

        public void UpdateBalance(string email, string codeWallet)
        {
        using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
            string sql = @"UPDATE WALLETCURRENT    SET Balance =FirstPctAction+SecondPctAction+ThirdPctAction+FourthPctAction+FifthPctAction "+
                                                " WHERE CodeWallet = @CodeWallet "+
                                                "  AND Email = @Email ";
               connection.Execute(sql, new{email = email, codeWallet = codeWallet});
            }
        }
        public List<WalletCurrent> GetAllWalletsCurrent(string codeWallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @" SELECT  CodeWallet ,"+
                                        "Email ,"+
                                        "DateUpdate,"+
                                        "FirstAction ,"+
                                        "FirstPrcAction ,"+
                                        "FirstPrcActionCurrent ,"+
                                        "FirstPctAction ,"+
                                        "SecondAction ,"+
                                        "SecondPrcAction ,"+
                                        "SecondPrcActionCurrent ,"+
                                        "SecondPctAction ,"+
                                        "ThirdAction ,"+
                                        "ThirdPrcAction,"+
                                        "ThirdPrcActionCurrent ,"+
                                        "ThirdPctAction,"+
                                        "FourthAction ,"+
                                        "FourthPrcAction,"+
                                        "FourthPrcActionCurrent ,"+
                                        "FourthPctAction,"+
                                        "FifthAction ,"+
                                        "FifthPrcAction ,"+
                                        "FifthPrcActionCurrent ,"+
                                        "FifthPctAction ,"+
                                        "FifthPctAction +FourthPctAction +ThirdPctAction +SecondPctAction + FirstPctAction as balance"+
                                  " FROM WALLETCURRENT "+
                                  " WHERE CodeWallet = @CodeWallet " +
                                  " ORDER BY 24 DESC";
                var result = connection.Query<WalletCurrent>(sql, new { codeWallet});
                return result.AsList();
            }
        }

        public List<WalletCurrent> GetWalletCurrent(string email,string codeWallet)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"SELECT  CodeWallet ,"+
                                        "Email ,"+
                                        "DateUpdate,"+
                                        "FirstAction ,"+
                                        "FirstPrcAction ,"+
                                        "FirstPrcActionCurrent ,"+
                                        "FirstPctAction ,"+
                                        "SecondAction ,"+
                                        "SecondPrcAction ,"+
                                        "SecondPrcActionCurrent ,"+
                                        "SecondPctAction ,"+
                                        "ThirdAction ,"+
                                        "ThirdPrcAction,"+
                                        "ThirdPrcActionCurrent ,"+
                                        "ThirdPctAction,"+
                                        "FourthAction ,"+
                                        "FourthPrcAction,"+
                                        "FourthPrcActionCurrent ,"+
                                        "FourthPctAction,"+
                                        "FifthAction ,"+
                                        "FifthPrcAction ,"+
                                        "FifthPrcActionCurrent ,"+
                                        "FifthPctAction ,"+
                                        "balance"+
                               " FROM WALLETCURRENT "+
                              " WHERE Email = @Email ";
                var result = connection.Query<WalletCurrent>(sql, new { email });
                return result.AsList();
         }
        }



    }

}