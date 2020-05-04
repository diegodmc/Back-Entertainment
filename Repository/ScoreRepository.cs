using System.Collections.Generic;
using Back_Entertainment.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System;

namespace Back_Entertainment.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly string _connectionString;
        public ScoreRepository(string connectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
        }

        public int GetRanking(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @" select email, sum(ScorePoint)"+
                                " from b3.score "+
                             " group by email "+
                             " order by 2 desc";
                var result = connection.Query<Score>(sql);
                int position = 0 ;
                foreach(var item in result)
                {
                    position =position+1;
                    if(item.Email.Equals(email))
                    {
                        return position;
                    }

                }
                return 0;
            }
        }
         public void CreateScore(Score score)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Score (email, NumberEntertainment, ScorePoint) VALUES(@email, @NumberEntertainment, @ScorePoint)";
                connection.Execute(sql, new {Email = score.Email, score = score.ScorePoint, NumberEntertainment = score.NumberEntertainment});
                
            }
        }


        public List<Score> GetScore(string email)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT email, ScorePoint, numberEntertainment FROM score WHERE Email = @Email ";
                var result =  connection.Query<Score>(sql, new {Email = email});
                if(result.AsList().Count == 0)
                  return null;
                else
                  return result.AsList();
            }
        }

        

        public List<Score> GetAllScore()
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string sql = @" select ifnull((select name from people p where p.email = s.email limit 1),s.email) as email, 
                                       sum(ScorePoint) as ScorePoint"+
                                " from b3.score s "+
                             " group by email "+
                             " order by 2 desc";
                var result =  connection.Query<Score>(sql);
                if(result.AsList().Count == 0)
                  return null;
                else
                  return result.AsList();
            }
        }

    }
}