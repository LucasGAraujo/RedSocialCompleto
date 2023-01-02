using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Domain.Entities {
    public class Perfil {

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Foto { get; set; }
        public ICollection<Post>? Posts { get; set; }

        [Display(Name = "Relacionamentos")]
        public ICollection<Relacionamento>? RelacionamentosA { get; set; }

        [Display(Name = "Relacionamentos")]
        public ICollection<Relacionamento>? RelacionamentosB { get; set; }
    }
}