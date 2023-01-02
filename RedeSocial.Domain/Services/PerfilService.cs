using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Domain.Services {
    public class PerfilService {

        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository) {

            _perfilRepository = perfilRepository;
        }

        public void CriarPerfil(Perfil perfil) {
            _perfilRepository.CriarPerfil(perfil);
        }

        public Perfil ConsultarPerfil(Guid id) {
            return _perfilRepository.ConsultarPerfil(id);
        }

        public ICollection<Perfil> ConsultarPerfils() {
            return _perfilRepository.ConsultarPerfils();
        }

        public bool AlterarPerfil(Perfil perfil) {

            var result = _perfilRepository.AlterarPerfil(perfil);
            if (result == 0) {
                return false;
            }
            return true;
        }

        public bool ExcluirPerfil(Guid id) {

            var result = _perfilRepository.ExcluirPerfil(id);
            if (result == 0) {
                return false;
            }
            return true;
        }
    }
}
