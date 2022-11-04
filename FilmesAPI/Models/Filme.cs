using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} não pode ser nulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0} não pode ser nulo")]
        public string Diretor { get; set; }

        [StringLength(30, ErrorMessage = "O genero não pode passar de 30 caracteres")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A {0} deve ter minimo 1 e maximo 600")]
        public int Duracao { get; set; }
    }
}