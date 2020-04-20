using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
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

        
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<Wallet>> Create([FromBody] Wallet model)
        {   
            if (model == null) return BadRequest(model);

            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            model.Email = User.Identity.Name;

            var wallet = GetWallet(User.Identity.Name, "1");
            if(wallet == null)
                _walletRepository.CreateWallet(model);
            else
               _walletRepository.UpdateWallet(model);
            return model;
        }
        [HttpGet]
        [Route("GetWallet")]
        [Authorize]
        public async Task<ActionResult<Wallet>> GetWallet()
        {   
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            return _walletRepository.GetWallet(User.Identity.Name, "1", 1);
        }

        public async Task<ActionResult<Wallet>> GetWallet(string email, string status)
        {   
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            
            return _walletRepository.GetWallet(User.Identity.Name, status, 1);
            
        }

    }
}