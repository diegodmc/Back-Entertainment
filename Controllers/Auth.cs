using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back_Entertainment.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Back_Entertainment.Services;
using Back_Entertainment.Repository;

namespace Back_Entertainment.Controllers
{
    [Route("v1/account")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody]User model)
        {
            var user = _userRepository.GetUser(model.Email);

            if(user == null)
              return NotFound();

            if(!Hash.VerifyPassword(model.Password, user.PasswordHash,user.PasswordSalt))
                return Unauthorized();
                
            model.Password = "";

            return TokenService.GenerateToken(user);
            
            
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Register([FromBody]User model)
        {

            var user = _userRepository.GetUser(model.Email);
            if(user != null)
                return new ConflictResult();
                
            byte[] passwordHash, passwordSalt;
            Hash.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            model.Password = "";
            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;

             _userRepository.CreateUser(model);

            return  TokenService.GenerateToken(model);
            
        }


        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

    }
}