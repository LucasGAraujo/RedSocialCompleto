using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces {
    public interface IPerfilRepository {

        public void CriarPerfil(Perfil perfil);
        public Perfil ConsultarPerfil(Guid id);
        public ICollection<Perfil> ConsultarPerfils();
        public int AlterarPerfil(Perfil perfil);
        public int ExcluirPerfil(Guid id);
    }
}
