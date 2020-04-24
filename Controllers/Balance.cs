using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Repository;
using System.Collections.Generic;
using System;

namespace Back_Entertainment.Controllers
{
    [Route("v1/balance")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceRepository _balanceRepository;
        public BalanceController(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<dynamic>> Create([FromBody] Balance model)
        {   
            if (model == null) return BadRequest(model);

            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            model.Email = User.Identity.Name;
            model.DateInput = "0";
            model.ValueInput = "0";
            model.DateOut = DateTime.Now.ToString();
            model.ValueOut = "15";


            _balanceRepository.CreateBalance(model);
               
            return model;
        }
        [HttpGet]
        [Route("GetBalance")]
        [Authorize]
        public async Task<ActionResult<List<Balance>>> GetBalance()
        {   
            if(User.Identity.Name == null)  return BadRequest("Usuário inválido");
            
            return _balanceRepository.GetAllBalances(User.Identity.Name);
        }

        [HttpGet]
        [Route("GetMoney")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetMoney()
        {   
            if(User.Identity.Name == null)  return BadRequest("Usuário inválido");
            var balance = _balanceRepository.GetAllBalances(User.Identity.Name);
            int over = 0;
            foreach(var item in balance)
            {
              over = Convert.ToInt32(item.ValueInput)- Convert.ToInt32(item.ValueOut);
            }
            
             if(over >= 15)
                return Ok(new {accountBalance = String.Format(" {0, 0:C2}", over), cust="R$ 15,00", updatedBalance = String.Format("{0, 0:C2}",(over-15)) });
             else
                return BadRequest();
            
        }  
    }
    
}