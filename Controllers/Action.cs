using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Repository;
using System.Collections.Generic;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Back_Entertainment.Controllers
{
    [Route("v1/action")]
    public class ActionController : ControllerBase
    {
        private readonly IActionB3Repository _actionb3Repository;
        private readonly IPriceActionRepository _priceactionRepository;

        private readonly IWalletCurrentRepository _walletCurrentRepository;

        public IConfiguration Configuration { get; }
        private readonly IWalletRepository _walletRepository;
        private static readonly HttpClient client = new HttpClient();
        public ActionController(IActionB3Repository actionb3Repository,IPriceActionRepository priceactionRepository,IWalletCurrentRepository walletCurrentRepository,IWalletRepository walletRepository,IConfiguration configuration)
        {
            _actionb3Repository = actionb3Repository;
            _priceactionRepository = priceactionRepository;
            _walletCurrentRepository = walletCurrentRepository;
            _walletRepository = walletRepository;
            Configuration = configuration;
        }
/*
        [HttpGet]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult> Create()
        {   
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://cotacoes.economia.uol.com.br/ws/asset/stock/list?size=10000"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   
                    var actionList = (MyClass)Newtonsoft.Json.JsonConvert.DeserializeObject(apiResponse, typeof(MyClass));

                    foreach(var action in actionList.data)
                    {
                      if(action.Code.Length ==8)
                      {
                            if(action.Code.EndsWith("3.SA") || action.Code.EndsWith("4.SA"))
                            {
                                 var actionB3 = new ActionB3();
                                 actionB3.Code = action.Code;
                                 actionB3.CompanyAbvName =null;
                                 actionB3.CompanyName = null;
                                 actionB3.Idt = action.Idt;
                                 actionB3.Name = null;
                                _actionb3Repository.CreateAction(actionB3);
                            }
                      }
                      
                    }
                    
                }
            }

            
            return null;
        }*/
        
        [HttpGet]
        [Route("UpdatePriceB3")]
        [Authorize]
        public async Task<ActionResult> UpdatePriceB3()
        {
             
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            
           // if(CanCall())
            {
                await UpdatePriceBDB3();
                return Ok();   
            }
            return BadRequest();   
        }

        public async Task UpdatePriceBDB3()
        {
            
            var list = _actionb3Repository.GetAllActions();
            var priceExist = _priceactionRepository.GetPriceAction("ITSA4");
            foreach(var item in list)
            {
                try
                {
                using (var httpClient = new HttpClient())
                    {
                        var url = "http://cotacoes.economia.uol.com.br/ws/asset/"+item.Idt+"/intraday?size=1";
                        using (var response = await httpClient.GetAsync(url))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                        
                            var action = (PriceActionData)Newtonsoft.Json.JsonConvert.DeserializeObject(apiResponse, typeof(PriceActionData));
                            
                            var obj = new PriceAction();
                            obj.Date = action.data[0].Date;
                            obj.High = action.data[0].High;
                            obj.Low = action.data[0].Low;
                            obj.Price = action.data[0].Price;
                            obj.Var = item.Code;
                            obj.Varpct = action.data[0].Varpct;
                            obj.Vol = action.data[0].Vol;
                            if(priceExist == null)
                             _priceactionRepository.CreatePriceAction(obj);
                            else 
                            _priceactionRepository.UpdatePriceAction(obj);
                        }
                    }
                }
                catch
                {

                }
            }
        }


        [HttpGet]
        [Route("StartEntertainment")]
        [Authorize]
        public async Task<ActionResult> StartEntertainment(string code)
        {
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            if(CanCall())
            {
                _walletCurrentRepository.CreateWalletCurrent(code);
                _walletCurrentRepository.UpdateWalletStart(code);
                UpdateWalletClient(code,"1");
                return Ok();   
            }
            return BadRequest();   
        }
        [HttpGet]
        [Route("StartUpdateWalletClient")]
        [Authorize]
        public async Task<ActionResult> StartUpdateWalletClient(string code)
        {
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            //if(CanCall())
            {
                code="1";
                UpdateWalletClient(code,"2");
                return Ok();   
            }
            return BadRequest();   
        }
        
        public void UpdateWalletClient(string code, string status)
        {
            var list = _walletCurrentRepository.GetAllWalletsCurrent(code);
            foreach(var item in list)
            {
                item.FirstPrcActionCurrent = _priceactionRepository.GetPriceAction(item.FirstAction).Price.Replace(".",",");
                item.SecondPrcActionCurrent = _priceactionRepository.GetPriceAction(item.SecondAction).Price.Replace(".",",");;
                item.ThirdPrcActionCurrent = _priceactionRepository.GetPriceAction(item.ThirdAction).Price.Replace(".",",");;
                item.FourthPrcActionCurrent = _priceactionRepository.GetPriceAction(item.FourthAction).Price.Replace(".",",");;
                item.FifthPrcActionCurrent = _priceactionRepository.GetPriceAction(item.FifthAction).Price.Replace(".",",");;
                item.FirstPrcAction = item.FirstPrcAction.Replace(".",",");
                item.SecondPrcAction = item.SecondPrcAction.Replace(".",",");
                item.ThirdPrcAction = item.ThirdPrcAction.Replace(".",",");
                item.FourthPrcAction = item.FourthPrcAction.Replace(".",",");
                item.FifthPrcAction = item.FifthPrcAction.Replace(".",",");
                _walletCurrentRepository.UpdateWalletCurrent(item);
            }
            
            
        }
       
        [HttpGet]
        [Route("UpdateClient")]
        [Authorize]
        public async Task<ActionResult> UpdateClient(string codeWallet)
        {

            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            
            if(CanCall())
            {
                var list = _walletCurrentRepository.GetAllWalletsCurrent(codeWallet);
                
                foreach(var item in list)
                {

                    item.FirstPrcActionCurrent = _priceactionRepository.GetPriceAction(item.FirstAction).Price;
                    item.SecondPrcActionCurrent = _priceactionRepository.GetPriceAction(item.SecondAction).Price;
                    item.ThirdPrcActionCurrent = _priceactionRepository.GetPriceAction(item.ThirdAction).Price;
                    item.FourthPrcActionCurrent = _priceactionRepository.GetPriceAction(item.FourthAction).Price;
                    item.FifthPrcActionCurrent = _priceactionRepository.GetPriceAction(item.FifthAction).Price;

                    _walletCurrentRepository.UpdateWalletCurrent(item);
                }
                return Ok();   
            }
            return BadRequest();   
        }
        
        [HttpGet]
        [Route("EndEntertainment")]
        [Authorize]
        public async Task<ActionResult> EndEntertainment(string code)
        {
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            if(CanCall())
            {
                _walletCurrentRepository.CreateWalletCurrent(code);
                _walletCurrentRepository.UpdateWalletStart(code);
                UpdateWalletClient(code,"1");
                return Ok();   
            }
             return BadRequest();   
        }
        
     
        
        [HttpGet]
        [Route("GetActions")]
        [Authorize]
        public async Task<ActionResult<List<ActionJson>>> GetActions()
        {   
            if(User.Identity.Name == null) return BadRequest("Usuário inválido");
            var result = new List<ActionJson>();
            if(CanCall())
            {
                    var actions = _actionb3Repository.GetAllActions();
                    foreach(var item in actions)
                    {
                        var obj = new ActionJson();
                        obj.Cd = item.Code.Replace(".SA","");
                        result.Add(obj);
                    }
            }
            return result;
        }
        private bool CanCall()
        {
            if(Configuration.GetConnectionString("User").Equals(User.Identity.Name))          
               return true;
            return false;
            
        }
    }

    public class ActionJson
    {
        public string Cd {get; set;}

    }
}