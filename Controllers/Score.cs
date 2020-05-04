using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Repository;
using System.Collections.Generic;
using System;

namespace Back_Entertainment.Controllers
{
    [Route("v1/score")]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IPeopleRepository _peopleRepository;
        public ScoreController(IScoreRepository scoreRepository,IPeopleRepository peopleRepository)
        {
            _scoreRepository = scoreRepository;
            _peopleRepository = peopleRepository;
        }

        
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        /*
        public async Task<ActionResult<Score>> Create([FromBody] Score model)
        {   
            if (model == null) return BadRequest(model);

            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            model.Email = User.Identity.Name;
            
            _scoreRepository.CreateScore(model);
            
            return model;
        }*/
        [HttpGet]
        [Route("GetScorePeople")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetScorePeople()
        {   
            if(User.Identity.Name == null)  return BadRequest("Usuário inválido");
            
            var result = _scoreRepository.GetScore(User.Identity.Name);
            int note =0;
            
            foreach(var item in result)
            {
                note = note+ Convert.ToInt32(item.ScorePoint);
            }
            var profile = GetProfile(result.Count, note);

            var position = _scoreRepository.GetRanking(User.Identity.Name);
            
            var name = _peopleRepository.GetPeople(User.Identity.Name).Name;

            if(result == null)
                return null;
            else 
                return new {name = name == null ? User.Identity.Name: name, position = position, profile = profile, note = note};
            
        }
        
        public string GetProfile(int qty, int note)
        {
            if(qty == 0)
              return "Iniciante";
            if((note/qty)>=8)
              return "Influenciador";
            if((note/qty) >=6 && (note/qty) <= 7)
                return "Promissor";
            if((note/qty) >=1 && (note/qty) <= 5)
               return "Determinado";
            else
               return "Socialista";
        }
        [HttpGet]
        [Route("GetScore")]
        [Authorize]
        public async Task<ActionResult<List<Score>>> GetScore()
        {   
            if(User.Identity.Name == null)  return BadRequest("Usuário inválido");
            
            var result = _scoreRepository.GetAllScore();

            int position = 0 ;
            foreach(var item in result)
            {
                position =position+1;
                item.Position = position.ToString();

            }
            if(result == null)
                return null;
            else 
                return result;
            
        }
    }
    
}