using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FruitStore.Data;
using FruitStore.Models;
using Microsoft.AspNetCore.Authorization;
using FruitStore.Repositories;
using FruitStore.Services;

namespace FruitStore.Controllers
{
    [ApiController]
    [Route("v1/login")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Authenticate(User model)
        {
            // Recupera o usuário
            var user = UserRepository.Get(model.Username, model.Password);

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
    }
}
