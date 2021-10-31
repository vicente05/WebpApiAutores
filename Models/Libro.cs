using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebpApiAutores.Validaciones;

namespace WebpApiAutores.Models
{
    [Table("libros")]
    public partial class Libro
    {
        public int id { get; set; }
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string titulo { get; set; }
    }
}