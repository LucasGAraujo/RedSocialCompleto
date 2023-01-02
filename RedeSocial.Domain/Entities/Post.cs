using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RedeSocial.Domain.Entities {
    public class Post {

        public Guid Id { get; set; }

        public string? Foto { get; set; }
        [Display(Name = "Legenda")]
        public string? Comentario { get; set; }    

        public DateTime Data { get; set; }

        public Perfil? Perfil { get; set; }

        public ICollection<Comentario>? Comentarios { get; set; }
    }
}
