

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebpApiAutores.Models
{
    [Table("autores")]
    public class Autor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Libro> libros { get; set; }
    }
}