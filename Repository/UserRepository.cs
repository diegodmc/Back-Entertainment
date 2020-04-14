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
                return connection.Query<User>("SELECT UserName,Role,PasswordHash,PasswordSalt FROM USER");
            }
        }

        public User GetUser(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return  connection.Query<User>("SELECT UserName,Role,PasswordHash,PasswordSalt FROM USER ").AsList().Find(e => e.Username == email);
            }
        }

         public void CreateUser(User user)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO USER (UserName,Role,PasswordHash,PasswordSalt) VALUES(@UserName,@Role,@PasswordHash,@PasswordSalt)";
                connection.Execute(sql, new {UserName = user.Username,Role = user.Role,PasswordHash = user.PasswordHash,PasswordSalt =user.PasswordSalt});
                
            }
        }
    }

}