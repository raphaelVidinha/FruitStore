using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FruitStore.Models;
using FruitStore.Repositories;
using FruitStore.Services;
using Microsoft.AspNetCore.Authorization;
using FruitStore.Data;

namespace FruitStore.Controllers
{
    [ApiController]
    [Route("v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;
        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Cart>> AddToCart(Cart model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            decimal Amount = 0;
            foreach(var item in model.Items)
            {
                var fruit = await _context.Fruits.FindAsync(item.FruitId);
                if(fruit.StockQuantity < item.Quantity){
                    return BadRequest("Existe um item no seu carrinho que está esgotado.");
                }
                Amount += (decimal)item.Quantity * fruit.Price;
                item.CartId = model.Id;
                _context.Items.Add(item);
            }
            model.Amount = Amount;
            _context.Carts.Add(model);
            await _context.SaveChangesAsync();
            await UpdateStockFruit(model);
            return model;
        }

        private async Task<bool> UpdateStockFruit(Cart model)
        {
            foreach(var item in model.Items)
            {
                var fruit = await _context.Fruits.FindAsync(item.FruitId);
                fruit.StockQuantity -= item.Quantity;
                _context.Fruits.Update(fruit);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
