using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitStore.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Fruit")]
        public int FruitId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}
