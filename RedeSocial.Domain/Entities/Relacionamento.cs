using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RedeSocial.Domain.Entities {
    public class Relacionamento {

        [Display(Name = "Perfil A")]
        public Guid PerfilIdA { get; set; }

        [Display(Name = "Perfil A")]
        public Perfil? PerfilA { get; set; }

        [Display(Name = "Perfil B")]
        public Guid PerfilIdB { get; set; }

        [Display(Name = "Perfil B")]
        public Perfil? PerfilB { get; set; }

    }
}
