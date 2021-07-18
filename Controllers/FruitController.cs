using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FruitStore.Data;
using FruitStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

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
        [Authorize]
        public async Task<ActionResult<List<Fruit>>> Get()
        {
            return await _context.Fruits.ToListAsync();
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Fruit>> Post(Fruit model)
        {
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
        [Authorize]
        public async Task<ActionResult<Fruit>> GetById(int id)
        {
            return await _context.Fruits.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        private async Task<bool> FruitExists(int id) {
            return await GetById(id) != null;
        }

        [HttpPut]
        [Route("id:int")]
        [Authorize]
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
            catch (DbUpdateConcurrencyException)
            {
                if (await FruitExists(id) != true)
                    return NotFound();
            }

            return fruit;
        }

        [HttpDelete]
        [Route("id:int")]
        [Authorize]
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

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> uploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                    using(var stream = new FileStream(fullPath, FileMode.Create)){
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(500);
            }
        }
    }
}
