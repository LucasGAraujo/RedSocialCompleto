using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories {
    public class PerfilRepository : IPerfilRepository {

        private readonly RedeSocialDbContext _context;

        public PerfilRepository(RedeSocialDbContext context, WithDapper dapper)
        {
            _context = context;
        }

        public Perfil ConsultarPerfil(Guid id) {
            return _context.Perfils_.Find(id);
        }

        public ICollection<Perfil> ConsultarPerfils() {
            return _context.Perfils_.ToList();
        }

        public void CriarPerfil(Perfil perfil) {
            _context.Add(perfil);
            _context.SaveChanges();
        }

        public int AlterarPerfil(Perfil perfil) {
            _context.Update(perfil);
            return _context.SaveChanges();
        }

        public int ExcluirPerfil(Guid id) {
            Perfil perfil = _context.Perfils_.Find(id);

            if (perfil == null) {
                return 0;
            }
            _context.Perfils_.Remove(perfil);
            return _context.SaveChanges();
        }
    }
}
