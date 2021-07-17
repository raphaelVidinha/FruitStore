using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FruitStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        public decimal Amount { get; set; }
    }
}
