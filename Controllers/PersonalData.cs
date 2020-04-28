using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Repository;
using System.Collections.Generic;
using System;

namespace Back_Entertainment.Controllers
{
    [Route("v1/personaldata")]
    public class PersonalDataController : ControllerBase
    {
        private readonly IPersonalDataRepository _personalDataRepository;
        public PersonalDataController(IPersonalDataRepository personalDataRepository)
        {
            _personalDataRepository = personalDataRepository;
        }

        
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<PersonalData>> Create([FromBody] PersonalData model)
        {   
            if (model == null) return BadRequest(model);

            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            model.Email = User.Identity.Name;
            
            var personal = GetPersonalData();

            if(personal == null)
                _personalDataRepository.CreatePersonalData(model);
            else 
                _personalDataRepository.UpdatePersonalData(model);
            
            return model;
        }
        [HttpGet]
        [Route("GetPersonalData")]
        [Authorize]
        public async Task<ActionResult<PersonalData>> GetPersonalData()
        {   
            if(User.Identity.Name == null)  return BadRequest("Usuário inválido");
            
            return _personalDataRepository.GetPersonalData(User.Identity.Name);
        }
    }
    
}