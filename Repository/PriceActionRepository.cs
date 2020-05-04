using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class PriceActionRepository : IPriceActionRepository
    {
        private readonly string _connectionString;
        public PriceActionRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        
        public PriceAction GetPriceAction(string code)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT Date  ,Price ,Low   ,High  ,Var   ,Varpct, Vol FROM priceaction WHERE Var = @Var";
                var result =  connection.Query<PriceAction>(sql, new {Var = code+".SA"});
                if(result.AsList().Count == 0)
                  return null;
                else
                  return result.AsList()[0];
            }
        }

         public void CreatePriceAction(PriceAction price)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO PriceAction (Date  ,Price ,Low   ,High  ,Var   ,Varpct, Vol   ) VALUES(@Date  ,@Price ,@Low   ,@High  ,@Var   ,@Varpct, @Vol   )";
                connection.Execute(sql, new { Date = price.Date, Price = price.Price, Low = price.Low, High = price.High, Var = price.Var, Varpct = price.Varpct, Vol = price.Vol});
                
            }
        }

        
         public void UpdatePriceAction(PriceAction price)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                
                string sql = @"UPDATE PriceAction  SET Date =@Date ,"+
                                                        "Price =@Price,"+
                                                        "Low =@Low ,"+
                                                        "High =@High ,"+
                                                        "Var =@Var ,"+
                                                        "Varpct =@Varpct ,"+
                                                        "Vol =@Vol "+
                                                " WHERE Var =  @Var ;";
                                               

                connection.Execute(sql, new {       Date = price.Date,
                                                    Price = price.Price,
                                                    Low = price.Low,
                                                    High = price.High,
                                                    Var = price.Var,
                                                    Varpct = price.Varpct,
                                                    Vol = price.Vol

                                              });
                
            }
        }


    }

}