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
        private readonly DataContext _context;
        public FruitController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Fruit>>> Get(){
            return await _context.Fruits.ToListAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Fruit>> Post(Fruit model){
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Fruits.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Fruit>> GetById(int id)
        {
            return await _context.Fruits.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        private async Task<bool> FruitExists(int id) {
            return await GetById(id) != null;
        }

        [HttpPut]
        [Route("id:int")]
        public async Task<ActionResult<Fruit>> Put(int id, Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return BadRequest();
            }

             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(fruit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (await FruitExists(id) != true)
                    return NotFound();
            }

            return fruit;
        }

        [HttpDelete]
        [Route("id:int")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var fruit = await _context.Fruits.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if(fruit == null){
                return NotFound();
            }

            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
