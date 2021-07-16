using System.ComponentModel.DataAnnotations;

namespace FruitStore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 10 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 10 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 6 e 12 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 6 e 12 caracteres.")]
        public string Password { get; set; }
    }
}
