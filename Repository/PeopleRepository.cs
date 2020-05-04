using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly string _connectionString;
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        
        public People GetPeople(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT id, email, name, doc, phone FROM people WHERE Email = @Email order by id desc";
                var result =  connection.Query<People>(sql, new {Email = email});
                if(result.AsList().Count == 0)
                  return null;
                else
                  return result.AsList()[0];
            }
        }

         public void CreatePeople(People people)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO People (email, name, doc, phone) VALUES(@email, @name, @doc, @phone)";
                connection.Execute(sql, new {Email = people.Email, Name = people.Name, Doc = people.Doc, Phone = people.Phone});
                
            }
        }

        
         public void UpdatePeople(People people)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                people.Id = GetPeople(people.Email).Id;
                
                string sql = @"UPDATE People  SET Email =@Email,"+
                                                        "Name =@Name ,"+
                                                        "Doc =@Doc,"+
                                                        "Phone =@Phone "+
                                                " WHERE Email =  @Email "+
                                                "   AND Id = @Id ;";
                                               

                connection.Execute(sql, new {       Id = people.Id,
                                                    Email = people.Email,  
                                                    Name = people.Name,  
                                                    Doc = people.Doc,
                                                    Phone = people.Phone
                                              });
                
            }
        }


    }

}