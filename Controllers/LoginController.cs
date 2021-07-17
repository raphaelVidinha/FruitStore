using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FruitStore.Models;
using FruitStore.Repositories;
using FruitStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace FruitStore.Controllers
{
    [ApiController]
    [Route("v1/login")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Authenticate(string username, string password)
        {
            // Recupera o usuário
            var user = UserRepository.Get(username, password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";
            
            // Retorna os dados
            return new
            {
                token = token
            };
        }

        [HttpGet]
        [Route("getUserName")]
        [Authorize]
        public string GetUserName()
        {
            return User.Identity.Name;
        }

        [HttpGet]
        [Route("verifyRoleUser")]
        [Authorize]
        public bool VerifyRoleUser(string role)
        {
            return User.IsInRole(role);
        }
    }
}
