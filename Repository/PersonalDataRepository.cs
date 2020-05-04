using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class PersonalDataRepository : IPersonalDataRepository
    {
        private readonly string _connectionString;
        public PersonalDataRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        
        public PersonalData GetPersonalData(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT id, email,    bank,  agency,  account,  digit,  accountBalance,  value FROM PersonalData WHERE Email = @Email order by id desc";
                var result =  connection.Query<PersonalData>(sql, new {Email = email});
                if(result.AsList().Count == 0)
                  return null;
                else
                  return result.AsList()[0];
            }
        }

         public void CreatePersonalData(PersonalData personal)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO PersonalData (email,   bank,  agency,  account,  digit,  accountBalance,  value) VALUES(@email,    @bank,  @agency,  @account,  @digit,  @accountBalance,  @value)";
                connection.Execute(sql, new {Email = personal.Email, 
                                              Bank = personal.Bank, 
                                            Agency = personal.Agency, 
                                           Account = personal.Account, 
                                             Digit = "99", 
                                    AccountBalance = personal.AccountBalance, 
                                             Value = personal.Value});
                
            }
        }

        
         public void UpdatePersonalData(PersonalData personal)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                personal.Id = GetPersonalData(personal.Email).Id;
                personal.Digit = "0";
                string sql = @"UPDATE PersonalData  SET Email =@Email,"+
                                                        "Bank =@Bank ,"+
                                                        "Agency =@Agency,"+
                                                        "Account =@Account ,"+
                                                        "Digit =@Digit ,"+
                                                        "AccountBalance = @AccountBalance,"+
                                                        "Value = @Value "+
                                                " WHERE Email =  @Email "+
                                                "   AND Id = @Id ;";
                                               

                connection.Execute(sql, new {       Id = personal.Id,
                                                    Email = personal.Email,  
                                                    Bank = personal.Bank,  
                                                    Agency = personal.Agency,  
                                                    Account = personal.Account,  
                                                    Digit = personal.Digit,  
                                                    AccountBalance = personal.AccountBalance,  
                                                    Value = personal.Value 
                                              });
                
            }
        }


    }

}