using System.ComponentModel.DataAnnotations;

namespace FruitStore.Models {
    public class Fruit{
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres.")]
        public string name { get; set; }
    }
}
