using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FruitStore.Data;
using FruitStore.Models;

namespace FruitStore.Controllers
{
    [ApiController]
    [Route("v1/fruits")]
    public class FruitController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Fruit>>> Get([FromServices] DataContext context){
            return await context.Fruits.ToListAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Fruit>> Post([FromServices] DataContext context, [FromBody] Fruit model){
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Fruits.Add(model);
            await context.SaveChangesAsync();
            return model;
        }
    }
}
