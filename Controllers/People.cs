using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Repository;
using System.Collections.Generic;
using System;

namespace Back_Entertainment.Controllers
{
    [Route("v1/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;
        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<People>> Create([FromBody] People model)
        {   
            if (model == null) return BadRequest(model);

            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            model.Email = User.Identity.Name;
            
            var people = GetPeople();

            if(people.Result == null)
                _peopleRepository.CreatePeople(model);
            else 
                _peopleRepository.UpdatePeople(model);
            
            return model;
        }
        [HttpGet]
        [Route("GetPeople")]
        [Authorize]
        public async Task<ActionResult<People>> GetPeople()
        {   
            if(User.Identity.Name == null)  return BadRequest("Usuário inválido");
            
            var result =  _peopleRepository.GetPeople(User.Identity.Name);
            if(result == null)
                return null;
            else 
                return result;
            

        }
        
    }
    
}