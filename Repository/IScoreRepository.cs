using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IScoreRepository
    {
        List<Score> GetAllScore();
        List<Score> GetScore(string email);
        void CreateScore(Score score);
        int GetRanking(string email);
       
    }
}