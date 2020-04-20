using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        public IEnumerable<User> GetAllUsers()
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<User>("SELECT Email,PasswordHash,PasswordSalt FROM USER");
            }
        }

        public User GetUser(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return  connection.Query<User>("SELECT Email,PasswordHash,PasswordSalt FROM USER ").AsList().Find(e => e.Email == email);
            }
        }

         public void CreateUser(User user)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO USER (Email,PasswordHash,PasswordSalt) VALUES(@Email,@PasswordHash,@PasswordSalt)";
                connection.Execute(sql, new {Email = user.Email,PasswordHash = user.PasswordHash,PasswordSalt =user.PasswordSalt});
                
            }
        }
    }

}