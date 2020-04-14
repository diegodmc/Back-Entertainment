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
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = _userRepository.GetUser(model.Username);

            if(!Hash.VerifyPassword(model.Password, user.PasswordHash,user.PasswordSalt))
                return null;
                
            model.Password = "";

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            
            return new
            {
                user = user,
                token = token
            };
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Register([FromBody]User model)
        {
            byte[] passwordHash, passwordSalt;
            Hash.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            model.Password = "";
            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;

             _userRepository.CreateUser(model);

            var token = TokenService.GenerateToken(model);
            
            return new
            {
                user = model.Username,
                token = token
            };
        }


        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}