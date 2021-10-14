using System.ComponentModel.DataAnnotations.Schema;

namespace WebpApiAutores.Models
{
    public class Libro
    {
        public int id { get; set; }
        public string titulo { get; set; }
        [ForeignKey("autor")]
        public int autor_id { get; set; }
        [ForeignKey("autor_id")]
        public virtual Autor autor { get; set; }
    }
}