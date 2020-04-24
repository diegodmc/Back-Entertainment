using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Repository;
using System.Collections.Generic;

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

            var wallet = GetWallet();
            
            bool canToEdit = false;
            bool cantToEdit = false;

            foreach(var item in wallet)
            {
                if(!item.StatusWallet.Equals("3"))
                {
                    if(item.StatusWallet.Equals("0") || item.StatusWallet.Equals("1"))
                    {
                        _walletRepository.UpdateWallet(model);
                        canToEdit=true;
                        model.StatusWallet = "99";
                    }
                    if(item.StatusWallet.Equals("2"))
                      cantToEdit= true;
                }
            }
            
            if(!canToEdit && !cantToEdit)
            {
                _walletRepository.CreateWallet(model);
                
            } 
            return model;
        }
        [HttpGet]
        [Route("GetWallet")]
        [Authorize]
        public async Task<ActionResult<Wallet>> GetWallet(string email)
        {   
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            
             var wallet = GetWallet();

            foreach(var item in wallet)
            {
                if(!item.StatusWallet.Equals("3"))
                {
                    if(item.StatusWallet.Equals("0") || item.StatusWallet.Equals("1") || item.StatusWallet.Equals("2"))
                        return item;
                }
            }
            return null;
        }

      

        [HttpGet]
        [Route("GetHeader")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetHeader(string email)
        {
            //0 monto a carteira
            //1 carteira paga mas nao inicio
            //2 carteira paga entretenimento iniciado
            //3 historico
            //4 Apenas entrou
            if (User.Identity.Name != null)
            {

                var wallet = GetWallet();
                
                if(wallet.Count == 0)
                {
                     return new 
                                {
                                   subheader ="Inicio segunda-feira às 09:00 ",
                                    header = "Monte sua carteira!"
                                };
                }
                foreach(var item in wallet)
                {
                    if(!item.StatusWallet.Equals("3"))
                    {
                        if(item.StatusWallet.Equals("0"))
                        {
                                return new
                                {
                                    subheader = "Aguardando confirmação do pagamento",
                                    header = "Participação à confirmar"
                                };
                        }
                        else if(item.StatusWallet.Equals("1")) 
                        {
                            return new
                                {
                                    subheader = "Alteração da carteira até segunda às 09:00",
                                    header = "Participação confirmada!"
                                };
                        }
                        else if(item.StatusWallet.Equals("2"))
                        {
                            return new
                                {
                                    subheader = "Encerramento sexta às 18:00",
                                    header = "Posição 10º Rendimento 2,45%"
                                };
                        }
                        
                    }
                }
                
                
            }

            return BadRequest("Usuário inválido");
        }
         
         public List<Wallet> GetWallet()
         {
            return _walletRepository.GetWallet(User.Identity.Name);
         }
    }
    public enum Status
    {
            //0 monto a carteira
            walletMounted = 0,
            walletPayNotStarted =1,
            walletPayStarted=2,
            history=3
    }
}