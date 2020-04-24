using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly string _connectionString;
        public BalanceRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        public List<Balance> GetAllBalances(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"SELECT Email,ValueInput,DateInput,ValueOut,DateOut FROM balance where email = @email order by id desc";
                var result = connection.Query<Balance>(sql, new {Email = email});
                return result.AsList();
            }
        }

         public void CreateBalance(Balance balance)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Balance (Email,ValueInput,DateInput,ValueOut,DateOut) VALUES(@Email,@ValueInput,@DateInput,@ValueOut,@DateOut)";
                connection.Execute(sql, new {Email = balance.Email,ValueInput = balance.ValueInput,DateInput =balance.DateInput,ValueOut = balance.ValueOut,DateOut =balance.DateOut });
                
            }
        }
    }

}