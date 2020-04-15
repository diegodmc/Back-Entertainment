using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Services;
using Back_Entertainment.Repository;

namespace Back_Entertainment.Controllers
{
    [Route("v1/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Create([FromBody]Wallet model)
        {
            if( User.Identity.Name == null)
                return BadRequest("Dados inválidos");
                
            return Ok();
        }


    }
}