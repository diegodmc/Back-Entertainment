using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Back_Entertainment.Repository
{
    public class ActionB3Repository : IActionB3Repository
    {
        private readonly string _connectionString;
        public ActionB3Repository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }
        public List<ActionB3> GetAllActions()
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"SELECT idt,code,name,companyName,companyAbvName FROM Action where code in(select var from priceaction pa)";
                var result = connection.Query<ActionB3>(sql);
                return result.AsList();
            }
        }

         public void CreateAction(ActionB3 action)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Action (idt,code,name,companyName,companyAbvName ) VALUES(@idt,@code,@name,@companyName,@companyAbvName )";
                connection.Execute(sql, new {Idt = action.Idt, Code = action.Code, Name = action.Name, CompanyName = action.CompanyName, CompanyAbvName= action.CompanyAbvName});
                
            }
        }
    }

}